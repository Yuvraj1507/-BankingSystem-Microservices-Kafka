using System;
namespace BankAccountService.MoneyTransfer.Common.Exceptions
{
    public class BankAccountNotFoundException:Exception
    {
        private static readonly string MESSAGE = "Bank account with given id does not exists";

        public BankAccountNotFoundException():base(MESSAGE)
        {
        }
    }
}
