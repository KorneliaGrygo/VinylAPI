using System;

namespace VinylAPI.Middleware.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string msg):base(msg)
        {

        }
    }
}
