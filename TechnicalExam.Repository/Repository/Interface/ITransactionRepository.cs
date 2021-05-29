using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Repository.Entity;

namespace TechnicalExam.Repository.Repository.Interface
{
    public interface ITransactionRepository
    {
        Task<List<Transactions>> GetTransactions();
        Task<Transactions> GetTransaction(int transactionId);
        Task<string> AddTransaction(Transactions transaction);
    }
}
