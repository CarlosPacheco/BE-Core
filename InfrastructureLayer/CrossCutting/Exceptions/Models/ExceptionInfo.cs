using Newtonsoft.Json;
using System;

namespace CrossCutting.Exceptions.Models
{
    public sealed class ExceptionInfo
    {
        /// <summary>
        /// HTTP Status Code
        /// </summary>
        public int? StatusCode { get; set; }

        /// <summary>
        /// Error Message
        /// </summary>
        public string Message { get; set; }

#if DEBUG || QA
        public string Stack { get; set; }
#endif
        public ExceptionInfo(int? statusCode, string message, Exception exception = null)
        {
            StatusCode = statusCode;
            Message = message;
#if DEBUG || QA
            Stack = exception.ToString();
            //$"{error}{Environment.NewLine}{exception.Message}{Environment.NewLine}{exception.StackTrace}"
            //$"{customException.Message}{Environment.NewLine}{customException}";
#endif       
        }

        public ExceptionInfo(int? statusCode, Exception exception) : this(statusCode, exception.Message, exception)
        {
        }

        public ExceptionInfo(string message, Exception exception) : this(null, message, exception)
        {
        }

        public ExceptionInfo(Exception exception) : this(null, exception.Message, exception)
        {
        }

        public ExceptionInfo(CustomException customException) : this(customException.Code, customException.Message, customException)
        {
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
