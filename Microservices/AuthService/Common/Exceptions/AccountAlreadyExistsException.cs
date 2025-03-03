using System;
namespace AuthService.Exceptions
{
    public class AccountAlreadyExistsException : Exception
    {
        private static readonly string MESSAGE = "Account already exists";

        public AccountAlreadyExistsException() : base(MESSAGE) { }
    }
}
