using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;

namespace TechnicalExam.Services.Services.Interface
{
    public interface IAccountServices
    {
        Task<string> CreateAccount(AccountsModel account);

        Task<AccountsModel> GetAccountById(int account);
    }
}
