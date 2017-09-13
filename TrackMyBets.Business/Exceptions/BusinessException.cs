using System;

namespace TrackMyBets.Business.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message)
            : base(message) {
            Log.Write(this);
        }

        public BusinessException(string message, Exception ex)
            : base(message, ex) {
            Log.Write(this);
        }
    }
}
