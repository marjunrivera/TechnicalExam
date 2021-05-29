using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;
using TechnicalExam.Services.Services.Interface;

namespace TechnicalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        [HttpPost("TransferMoney")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionsModel transactions)
        {
            var response = await _transactionServices.TransferMoney(transactions);

            return Ok(response);
        }

        [HttpGet("GetTransactions")]
        public async Task<IActionResult> GetT()
        {
            var response = await _transactionServices.GetTransactions();

            return Ok(response);
        }
    }
}
