using System;

namespace CrossCutting.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException() : base("Bad request or invalid data") 
        {
        }

        public BadRequestException(string message, int? code = null) : base(message, code) 
        {
        }

        public BadRequestException(string message, Exception innerException, int? code = null) : base(message, innerException, code) 
        { 
        }

        protected BadRequestException(string message) : base(message) 
        {
        }
    }
}
