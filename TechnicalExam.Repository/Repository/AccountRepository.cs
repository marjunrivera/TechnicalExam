using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Repository;
using TechnicalExam.Repository.Entity;
using TechnicalExam.Repository.Repository.Interface;

namespace TechnicalExam.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TechnicalExamContext _dbContext;

        public AccountRepository(TechnicalExamContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Accounts>> GetAccounts()
        {
            return await _dbContext.Accounts.ToListAsync();
        }

        public async Task<Accounts> GetAccount(int accountId)
        {
            return await _dbContext.Accounts
                .FirstOrDefaultAsync(e => e.Id == accountId);
        }

        public async Task<string> AddAccount(Accounts account)
        {
            var result = await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return result.Entity.Id.ToString();
        }
        public async Task<Accounts> GetAccountByUsername(string username)
        {
            return await _dbContext.Accounts
                .FirstOrDefaultAsync(e => e.Username == username);
        }
    }
}
