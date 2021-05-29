using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TechnicalExam.Services.Models;
using TechnicalExamClient.Helper;

namespace TechnicalExamClient.Controllers
{
    public class TransactionController : Controller
    {

        TechnicalExamAPIHelper _api = new TechnicalExamAPIHelper();
        // GET: Transaction
        public async Task<IActionResult> Index()
        {
            List<TransactionsModel> transactions = new List<TransactionsModel>();
            HttpClient client = _api.InitializeAPIConnection();
            HttpResponseMessage responseMessage = await client.GetAsync("api/Transaction/GetTransactions");
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync().Result;
                transactions = JsonConvert.DeserializeObject<List<TransactionsModel>>(result);
            }

            return View(transactions);
        }

        public async Task<IActionResult> Create(TransactionsModel transactionsModel, string create)
        {
            ResponseMessage message = new ResponseMessage();

            if (create != null)
            {
                HttpClient client = _api.InitializeAPIConnection();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var myContent = JsonConvert.SerializeObject(transactionsModel);
                var content = new StringContent(myContent, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync("api/Transaction/TransferMoney", content);
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
            }
            else
            {
                return View();

            }
        }
    }
}
