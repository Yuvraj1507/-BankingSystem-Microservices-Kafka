using System;

namespace BankAccountService.Common.Exceptions
{
    public class BalanceTooLowException : Exception
    {
        private static readonly string MESSAGE = "The transfer amount exceeds the account balance";

        public BalanceTooLowException() : base(MESSAGE)
        {
        }
    }
}
