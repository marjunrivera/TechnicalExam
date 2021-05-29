using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Repository.Entity;
using TechnicalExam.Repository.Repository.Interface;
using TechnicalExam.Services.Models;
using TechnicalExam.Services.Services.Interface;

namespace TechnicalExam.Services.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        public TransactionServices(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public async Task<ResponseMessage> TransferMoney(TransactionsModel transactionModel)
        {
            try
            {
                Transactions transaction = new Transactions
                {
                    SourceAccountId = transactionModel.SourceAccountId,
                    DestinationAccountId = transactionModel.DestinationAccountId,
                    TransferAmount = transactionModel.TransferAmount
                };

                var transactionResult = await _transactionRepository.AddTransaction(transaction);

                return new ResponseMessage { result = transactionResult , message = "Transaction Successful!"};
            }
            catch (Exception ex)
            {
                return new ResponseMessage { result = null, isError = true, message = ex.Message };
            }
        }
        public async Task<List<TransactionsModel>> GetTransactions()
        {
            var response = await _transactionRepository.GetTransactions();

            if (response != null)
            {
                List<TransactionsModel> transactions = new List<TransactionsModel>();

                response.ForEach(x => {
                    transactions.Add(new TransactionsModel
                    {
                        Id = x.Id,
                        SourceAccountId = x.SourceAccountId,
                        DestinationAccountId = x.DestinationAccountId,
                        TransferAmount = x.TransferAmount
                    });
                });

                return transactions;
            }
            else
            {
                return new List<TransactionsModel>();
            }
        }

    }
}
