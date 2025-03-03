using System;
namespace AuthService.Exceptions
{
    public class BadCredentialsException : Exception
    {
        private static readonly string MESSAGE = "Bad credentials given. Cannot authorize.";

        public BadCredentialsException() : base(MESSAGE) { }
    }
}
