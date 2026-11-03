using System.Net;

namespace SmartBite.KitchenManagement.Application.Common.Exceptions
{
    [Serializable]
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;
        public BusinessException()
        {
        }

        public BusinessException(string? message) : base(message)
        {
        }

        public BusinessException(string? message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public BusinessException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
