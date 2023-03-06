using System;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace api;

public class GetData
{
    private readonly ILogger<GetData> _logger;

    public GetData(ILogger<GetData> logger)
    {
        _logger = logger;
    }

    [FunctionName("GetData")]
    [OpenApiOperation("Run", new[] { "name" })]
    [OpenApiRequestBody(MediaTypeNames.Application.Json, typeof(Person))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "The OK response")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
        Person req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        Response response = new() { Message = $"Hello, {req.FirstName} {req.LastName}. The time is {DateTime.Now:HH:mm:ss}." };

        return new OkObjectResult(response);
    }
}

public record Person(string FirstName, string LastName) { }

public class Response
{
    public string Message { get; init; }
}
