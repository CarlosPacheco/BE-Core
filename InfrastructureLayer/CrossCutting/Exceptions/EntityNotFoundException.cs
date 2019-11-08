using System;

namespace CrossCutting.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException() : base("Target entity was not found")
        {
        }

        public EntityNotFoundException(string message, int? code = null) : base(message, code)
        {
        }

        public EntityNotFoundException(string message, Exception innerException, int? code) : base(message, innerException, code)
        {
        }

        protected EntityNotFoundException(string message) : base(message)
        {
        }
    }
}