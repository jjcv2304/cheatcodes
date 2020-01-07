using System;
using Microsoft.AspNetCore.Http;

namespace Api.Logs.Middleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}