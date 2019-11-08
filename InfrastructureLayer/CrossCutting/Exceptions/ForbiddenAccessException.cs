using System;

namespace CrossCutting.Exceptions
{
    [Serializable]
    public class ForbiddenAccessException : CustomException
    {
        public ForbiddenAccessException() : base("No permission to execute action or to access resource")
        {
        }

        public ForbiddenAccessException(string message, int? code = null) : base(message, code)
        {
        }

        public ForbiddenAccessException(string message, Exception innerException, int? code) : base(message, innerException, code)
        {
        }

        protected ForbiddenAccessException(string message) : base(message)
        {
        }
    }
}