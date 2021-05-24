using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Repository.Entity;

namespace TechnicalExam.Repository.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Accounts>> GetAccounts();
        Task<Accounts> GetAccount(int accountId);
        Task<string> AddAccount(Accounts account);
        Task<Accounts> GetAccountByUsername(string username);
    }
}
