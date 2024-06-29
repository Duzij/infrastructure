using System.Runtime.Serialization;

namespace Library.Domain
{
    [Serializable]
    internal class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException()
        {
        }

        public override string Message => "Entity state is invalid.";

        public InvalidEntityStateException(string message) : base(message)
        {
        }

        public InvalidEntityStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEntityStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}