using System;

namespace CrossCutting.Exceptions
{
    [Serializable]
    public class IdentityConfigurationException : CustomException
    {
        public IdentityConfigurationException() : base("User Identity bad/missing configuration")
        {
        }

        public IdentityConfigurationException(string message, int? code = null) : base(message, code)
        {
        }

        public IdentityConfigurationException(string message, Exception innerException, int? code) : base(message, innerException, code)
        {
        }

        protected IdentityConfigurationException(string message) : base(message)
        {
        }
    }
}