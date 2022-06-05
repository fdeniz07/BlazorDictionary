using System.Runtime.Serialization;

namespace BlazorDictionary.Common.Infrastructure.Exceptions
{
    public class DatabaseValidationsException:Exception
    {
        public DatabaseValidationsException()
        {
        }

        protected DatabaseValidationsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DatabaseValidationsException(string? message) : base(message)
        {
        }

        public DatabaseValidationsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
