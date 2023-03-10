using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Alexandria.Api.Functions;

public class GenerateException
{
    private readonly ILogger<GenerateException> _logger;

    public GenerateException(ILogger<GenerateException> log)
    {
        _logger = log;
    }

    [FunctionName("GenerateException")]
    [OpenApiOperation(nameof(GenerateException), FunctionCategories.TestFunctions)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "The OK response")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
        HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        throw new Exception("Shit hit the fan!");
    }
}
