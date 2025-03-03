using System;
using System.Net;
using BankAccountService.Common.Models;

namespace BankAccountService.CommandsAndQueries.CreateBankAccount
{
    public class GetAccountBalanceResponse:ResponseModel
    {

        public GetAccountBalanceResponse(HttpStatusCode status, float balance):base(status)
        {
            Data = new GetAccountBalanceResponseData(balance);
        }
    }

    public class GetAccountBalanceResponseData
    {
        public float Balance { get; private set; }

        public GetAccountBalanceResponseData(float balance)
        {
            Balance = balance;
        }
    }
}
