using System;

namespace BankAccountService.MoneyTransfer.Common.Exceptions
{
    public class TransferToSameAccountException : Exception
    {
        private static readonly string MESSAGE = "Cannot transfer money to the same account";

        public TransferToSameAccountException() : base(MESSAGE)
        {
        }
    }
}
