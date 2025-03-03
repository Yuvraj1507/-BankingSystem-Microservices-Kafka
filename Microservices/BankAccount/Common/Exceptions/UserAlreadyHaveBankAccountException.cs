using System;
namespace BankAccountService.Common.Exceptions
{
    public class UserAlreadyHaveBankAccountException:Exception
    {
        private static readonly string MESSAGE = "User already have bank account";

        public UserAlreadyHaveBankAccountException():base(MESSAGE)
        {
        }
    }
}
