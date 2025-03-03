using System;
using System.Net;
using BankAccountService.Common.Models;

namespace BankAccountService.CommandsAndQueries.CreateBankAccount
{

    public class CreateBankAccountResponse : ResponseModel
    {
        public CreateBankAccountResponse(HttpStatusCode status, Guid bankAccountId) : base(status)
        {
            Data = new CreateBankAccountResponseData(bankAccountId);
        }
    }

    public class CreateBankAccountResponseData
    {
        public Guid bankAccountId { get; private set; }

        public CreateBankAccountResponseData(Guid bankAccountId)
        {
            this.bankAccountId = bankAccountId;
        }
    }
}
