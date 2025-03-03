using System;
using System.Net;
using BankAccountService.Common.Models;

namespace BankAccountService.CommandsAndQueries.CreateTransfer
{
    public class SendMoneyTransferResponse : ResponseModel
    {
        public SendMoneyTransferResponse(HttpStatusCode status) : base(status)
        {
        }
    }
}