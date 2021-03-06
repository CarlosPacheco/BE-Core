﻿using System;

namespace CrossCutting.Exceptions
{
    [Serializable]
    public class DataAccessException : CustomException
    {
        public DataAccessException() : base("A data access error has occurred")
        {
        }

        public DataAccessException(string message, int? code = null) : base(message, code)
        {
        }

        public DataAccessException(string message, Exception innerException, int? code = null) : base(message, innerException, code)
        {
        }

        protected DataAccessException(string message) : base(message)
        {
        }
    }
}