using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalExam.Services.Models
{
    public class ResponseMessage
    {
        public object result { get; set; }
        public bool isError { get; set; }
        public string message { get; set; }
    }
}
