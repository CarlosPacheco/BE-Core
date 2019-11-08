using System;

namespace CrossCutting.Exceptions
{
    public class MimeMultipartException : CustomException
    {
        public MimeMultipartException() : base("The request do not contains multipart/form-data or invalid data in request") { }

        public MimeMultipartException(string message, int? code = null) : base(message, code) { }

        public MimeMultipartException(string message, Exception innerException, int? code = null) : base(message, innerException, code) { }

        protected MimeMultipartException(string message) : base(message) { }
    }
}
