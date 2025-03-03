using System;
using System.Net;

namespace BankAccountService.Common.Models
{
    public class ResponseModel
    {
        public HttpStatusCode Status { get; private set; }
        public object Data { get; set; }

        public ResponseModel(HttpStatusCode status)
        {
            Status = status;
        }

    }
}
