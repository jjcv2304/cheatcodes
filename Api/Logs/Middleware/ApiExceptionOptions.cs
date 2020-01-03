using System;
using Microsoft.AspNetCore.Http;

namespace Presentation.Logs.Middleware
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}