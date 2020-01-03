using System.Net.Http;
using Application;
using Application.Utils;
using Castle.Core.Logging;
using CSharpFunctionalExtensions;
using Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Logs.Attributes;
using Presentation.Utils;
// ReSharper disable SuggestVarOrType_SimpleTypes

namespace Presentation.Categories
{
    
    public class ApiTestController : BaseController
    {
        private readonly Messages _messages;
        private ILogger<ApiTestController> _logger;

        public ApiTestController(Messages messages, ILogger<ApiTestController> logger) : base()
        {
            _logger = logger;
            _messages = messages;
        }


        [HttpGet]
        [Route("api/test/getError")]
        public IActionResult GetError(HttpRequestMessage request)
        {
            
            var ex = new LoggerException("Api Failure");

            ex.Data.Add("API Route", $"GET {request.RequestUri}");
            ex.Data.Add("API Status", (int)Response.StatusCode);

            throw ex;
        }
    }
}
