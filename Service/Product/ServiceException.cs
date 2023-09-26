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
    }
}