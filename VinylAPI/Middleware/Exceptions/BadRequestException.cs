using System;

namespace VinylAPI.Middleware.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
              
        }
    }
}
