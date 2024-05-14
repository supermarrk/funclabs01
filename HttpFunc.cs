using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace funclabs01
{
    public class HttpFunc
    {
        private readonly ILogger<HttpFunc> _logger;

        public HttpFunc(ILogger<HttpFunc> logger)
        {
            _logger = logger;
        }

        [Function("HttpFunc")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
