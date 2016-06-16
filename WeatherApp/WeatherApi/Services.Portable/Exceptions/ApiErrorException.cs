using System;

namespace Services.Portable.Exceptions
{
    public class ApiErrorException : Exception
    {
        public ApiErrorException(string errorMessage, string errorCode) : base(errorMessage)
        {
            ErrorCode = errorCode;
        }

        public ApiErrorException(string errorMessage, string errorCode, Exception innerException) : base(errorMessage, innerException)
        {
            ErrorCode = errorCode;
        }

        public string ErrorCode { get;  }
    }
}