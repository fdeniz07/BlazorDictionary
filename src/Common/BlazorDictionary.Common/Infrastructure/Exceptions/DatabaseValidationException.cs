using System.Runtime.Serialization;

namespace BlazorDictionary.Common.Infrastructure.Exceptions
{
    public class DatabaseValidationException:Exception
    {
        public DatabaseValidationException()
        {
        }

        protected DatabaseValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public DatabaseValidationException(string? message) : base(message)
        {
        }

        public DatabaseValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
