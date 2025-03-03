using System;

namespace BankAccountService.MoneyTransfer.Common.Exceptions
{
    public class BalanceTooLowException : Exception
    {
        private static readonly string MESSAGE = "The transfer amount exceeds the account balance";

        public BalanceTooLowException() : base(MESSAGE)
        {
        }
    }
}
