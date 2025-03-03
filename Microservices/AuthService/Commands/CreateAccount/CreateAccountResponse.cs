using System;
using System.Net;
using AuthService.Common.Models;

namespace AuthService.Commands.CreateAccount
{
    public class CreateAccountResponse:ResponseModel
    {
        public CreateAccountResponse(HttpStatusCode status, string userId):base(status)
        {
            Data = new CreateAccountResponseData(userId);

        }
    }

    public class CreateAccountResponseData
    {
        public string userId { get; private set; }

        public CreateAccountResponseData(string userId)
        {
            this.userId = userId;
        }
    }
}
