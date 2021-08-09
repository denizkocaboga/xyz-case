using System;
using System.Collections.Generic;
using System.Net;

namespace XyzCase.Tweeldata.ApiClient.Models
{
    public class GetPricesResponse : ApiClientResponse<IDictionary<string, Symbol>>
    {
        public GetPricesResponse(HttpStatusCode statusCode, IDictionary<string, Symbol> result)
            : base(statusCode, result) { }

        public GetPricesResponse(HttpStatusCode statusCode, Exception exception, string message, string detail)
        : base(statusCode, exception, message, detail) { }
    }
    public class ApiClientResponse<T>
    {
        public ApiClientResponse(bool isSuccess, HttpStatusCode statusCode)
        {
            IsSuccess = isSuccess;
            StatusCode = statusCode;
        }

        public ApiClientResponse(HttpStatusCode statusCode, T result) : this(true, statusCode)
        {
            Result = result;
        }

        public ApiClientResponse(HttpStatusCode statusCode, Exception exception, string message, string detail)
            : this(false, statusCode)
        {
            Exception = exception;
            Message = message;
            Detail = detail;
        }

        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; }
        public string Message { get; set; }
        public string Detail { get; set; }
        public Exception Exception { get; set; }
        public T Result { get; set; }
    }
}