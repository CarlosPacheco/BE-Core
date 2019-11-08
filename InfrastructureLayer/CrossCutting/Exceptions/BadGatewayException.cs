using System;

namespace CrossCutting.Exceptions
{
    public class BadGatewayException : CustomException
    {
        public BadGatewayException() : base("Bad gateway or proxy response") { }

        public BadGatewayException(string message, int? code = null) : base(message, code) { }

        public BadGatewayException(string message, Exception innerException, int? code = null) : base(message, innerException, code) { }

        protected BadGatewayException(string message) : base(message) { }
    }
}
