using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;
using TechnicalExam.Services.Services.Interface;

namespace TechnicalExam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }
        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountsModel account)
        {
            var result = await _accountServices.CreateAccount(account);

            return Ok(result);
        }

        [HttpGet("GetAccountById")]
        public async Task<IActionResult> GetAccountById([FromQuery] int accountId)
        {
            var result = await _accountServices.GetAccountById(accountId);

            return Ok(result);
        }

        [HttpGet("GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            var result = await _accountServices.GetAccounts();

            return Ok(result);
        }
    }
}
