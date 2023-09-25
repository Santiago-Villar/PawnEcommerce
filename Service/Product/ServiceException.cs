using System.Runtime.Serialization;

namespace Service.Product
{
    [Serializable]
    public class ServiceException : System.Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string? message) : base(message)
        {
        }

        public ServiceException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}