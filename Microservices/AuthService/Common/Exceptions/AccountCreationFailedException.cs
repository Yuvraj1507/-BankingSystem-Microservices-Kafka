using System;
namespace AuthService.Exceptions
{
    public class AccountCreationFailedException : Exception
    {
        private static readonly string message = "Something went wrong during account creation";

        public AccountCreationFailedException() : base(message) { }
    }
}
