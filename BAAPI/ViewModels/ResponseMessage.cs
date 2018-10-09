using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAAPI.ViewModels
{
    public class ResponseMessage
    {
        public string Message { get; set; }
        public bool Successfull { get; set; }
        public string AccountNumber { get; set; }
        public double? Balance { get; set; }
        public string Currency { get; set; }
    }
}