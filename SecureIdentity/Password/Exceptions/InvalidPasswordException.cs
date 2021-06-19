using System;

namespace SecureIdentity.Password.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message)
        {
        }
    }
}
