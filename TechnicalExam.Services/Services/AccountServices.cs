using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechnicalExam.Repository.Entity;
using TechnicalExam.Repository.Repository.Interface;
using TechnicalExam.Services.Models;
using TechnicalExam.Services.Services.Interface;

namespace TechnicalExam.Services.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<string> CreateAccount(AccountsModel request)
        {
            string response = string.Empty;

            try
            {
                var tempAccount = await GetAccountByUsername(request.Username);

                if (tempAccount != null && tempAccount.Username != request.Username)
                {
                    Accounts account = new Accounts
                    {
                        Username = request.Username,
                        InitialBalance = request.InitialBalance
                    };

                    response = await _accountRepository.AddAccount(account);
                }
                else
                {
                    throw new Exception("Username already exist.");
                }

                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<AccountsModel> GetAccountById(int accountId)
        {
            var response = await _accountRepository.GetAccount(accountId);

            AccountsModel account = new AccountsModel
            {
                Id = response.Id,
                Username = response.Username,
                InitialBalance = response.InitialBalance
            };

            return account;
        }

        public async Task<AccountsModel> GetAccountByUsername(string username)
        {
            var response = await _accountRepository.GetAccountByUsername(username);

            if (response != null)
            {
                return new AccountsModel
                {
                    Id = response.Id,
                    Username = response.Username,
                    InitialBalance = response.InitialBalance
                };
            }
            else
            {
                return new AccountsModel();
            }
        }
    }
}
