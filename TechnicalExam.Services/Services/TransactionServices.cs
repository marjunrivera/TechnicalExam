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
        public async Task<string> TransferMoney(TransactionsModel transactionModel)
        {
            Transactions transaction = new Transactions
            {
                SourceAccountId = transactionModel.SourceAccountId,
                DestinationAccountId = transactionModel.DestinationAccountId,
                TransferAmount = transactionModel.TransferAmount
            };

            var transactionResult = await _transactionRepository.AddTransaction(transaction);
            
            return transactionResult;
        }
    }
}
