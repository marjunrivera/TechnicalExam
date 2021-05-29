using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TechnicalExamClient.Helper
{
    public class TechnicalExamAPIHelper
    {
        public HttpClient InitializeAPIConnection()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:41746");

            return client;
        }
    }
}
