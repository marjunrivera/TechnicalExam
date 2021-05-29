using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;

namespace TechnicalExam.Services.Services.Interface
{
    public interface ITransactionServices
    {
        Task<ResponseMessage> TransferMoney(TransactionsModel transactionModel);
        Task<List<TransactionsModel>> GetTransactions();
    }
}
