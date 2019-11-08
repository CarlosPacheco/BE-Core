using System;

namespace CrossCutting.Exceptions
{
    public abstract class CustomException : Exception, ICustomException
    {
        public int? Code { get; set; }

        protected CustomException(string message) : base(message)
        {
            Code = null;
        }

        protected CustomException(string message, int? code = null) : base(message)
        {
            Code = code;
        }

        protected CustomException(string message, Exception innerException, int? code = null) : base(message, innerException)
        {
            Code = code;
        }
    }
}
