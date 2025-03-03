using System;
using System.Net;
using AuthService.Common.Models;

namespace AuthService.Commands.CreateAccount
{
    public class LoginResponse:ResponseModel
    {
        public LoginResponse(HttpStatusCode status, string token, DateTime validTill):base(status)
        {
            Data = new LoginResponseData(token, validTill);

        }
    }

    public class LoginResponseData
    {
        public string Token { get; private set; }
        public DateTime ValidTo { get; set; }

        public LoginResponseData(string token, DateTime validTo)
        {
            Token = token;
            ValidTo = ValidTo;
        }
    }
}
