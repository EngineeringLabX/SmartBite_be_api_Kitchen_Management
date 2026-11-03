using System.Net;

namespace SmartBite.KitchenManagement.Application.Common.Wrapper
{
    public class SBResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public static SBResponse<T> Success(T data, string? message = null)
        {
            return new SBResponse<T> { IsSuccess = true, Data = data, Message = message, StatusCode = HttpStatusCode.OK };
        }

        public static SBResponse<T> Failure(string message, HttpStatusCode statusCode)
        {
            return new SBResponse<T> { IsSuccess = false, Message = message, StatusCode = statusCode };
        }
    }
}
