using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Repository;
using TechnicalExam.Repository.Entity;
using TechnicalExam.Repository.Repository.Interface;

namespace TechnicalExam.Repository.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TechnicalExamContext _dbContext;

        public TransactionRepository(TechnicalExamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transactions>> GetTransactions()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transactions> GetTransaction(int transactionId)
        {
            return await _dbContext.Transactions
                .FirstOrDefaultAsync(e => e.Id == transactionId);
        }

        public async Task<string> AddTransaction(Transactions transaction)
        {
            int transactionId = 0;
            using (var context = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await _dbContext.Transactions.AddAsync(transaction);
                    var destinationAccount = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == transaction.DestinationAccountId);
                    var sourceAccount = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == transaction.SourceAccountId);

                    if (destinationAccount != null)
                        destinationAccount.InitialBalance += transaction.TransferAmount;
                    else
                        throw new Exception($"Transaction failed: No account was found using Id: {transaction.DestinationAccountId}");

                    if (sourceAccount != null)
                        sourceAccount.InitialBalance -= transaction.TransferAmount;
                    else
                        throw new Exception($"Transaction failed: No account was found using Id: {transaction.SourceAccountId}");

                    //validating amounts source and destination
                    if (sourceAccount.InitialBalance < 0)
                    {
                        throw new Exception($"Transaction failed: Insuffucient Balance for Account {sourceAccount.Username}. " +
                            $"Account balance cannot be lower than 0.");
                    }

                    await _dbContext.SaveChangesAsync();
                    await context.CommitAsync();
                    transactionId = result.Entity.Id;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var inEntry = ex.Entries.Single();
                    var dbEntry = inEntry.GetDatabaseValues();

                    if (dbEntry == null)
                        throw new Exception("Transaction failed: Account was deleted by another user.");


                    var inModel = inEntry.OriginalValues.ToObject() as Accounts;
                    var dbModel = dbEntry.ToObject() as Accounts;

                    var conflicts = string.Empty;

                    if (inModel.InitialBalance != dbModel.InitialBalance)
                        conflicts = ($"Transaction failed: Initial Balance for account named {inModel.Username} " +
                            $"is update by another transaction from {inModel.InitialBalance} to {dbModel.InitialBalance}. " +
                            $"Please try again.");

                    await context.RollbackAsync();
                    throw new Exception(conflicts);
                }
                catch (Exception ex)
                {
                    await context.RollbackAsync();
                    throw ex;
                }
            }

            return transactionId.ToString();
        }

    }
}
