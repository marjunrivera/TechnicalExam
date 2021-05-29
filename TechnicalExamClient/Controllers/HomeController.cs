using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;
using TechnicalExamClient.Helper;
using TechnicalExamClient.Models;

namespace TechnicalExamClient.Controllers
{
    public class HomeController : Controller
    {
        TechnicalExamAPIHelper _api = new TechnicalExamAPIHelper();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<AccountsModel> accounts = new List<AccountsModel>();
            HttpClient client = _api.InitializeAPIConnection();
            HttpResponseMessage responseMessage = await client.GetAsync("api/Account/GetAccounts");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                accounts = JsonConvert.DeserializeObject<List<AccountsModel>>(result);
            }

            return View(accounts);
        }

        public async Task<IActionResult> Create(AccountsModel accountsModel, string create)
        {
            ResponseMessage message = new ResponseMessage();
            if (create != null)
            {
                List<AccountsModel> accounts = new List<AccountsModel>();
                HttpClient client = _api.InitializeAPIConnection();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var myContent = JsonConvert.SerializeObject(accountsModel);
                var content = new StringContent(myContent, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("api/Account/CreateAccount", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    message = JsonConvert.DeserializeObject<ResponseMessage>(responseMessage.Content.ReadAsStringAsync().Result);
                }

                if (!message.isError)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = message.message;
                    return View();
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
