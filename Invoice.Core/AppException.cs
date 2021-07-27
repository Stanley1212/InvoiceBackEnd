using Invoice.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Core
{
    public class AppException : Exception
    {
        private readonly int _code;

        public AppException() : base() { }

        public AppException(MessageCode code, string message) : base(message)
        {
            this._code = (int)code;
        }

        public AppException(string message, AppException innerException) : base(message, innerException) { }

        public int Code
        {
            get { return this._code; }
        }

        public static AppException Create(MessageCode code, string message)
        {
            switch (code)
            {
                case MessageCode.GeneralException:
                    break;
            }
            return new AppException(code, message);
        }
    }
}
