using System;
using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace api;

public class GetData
{
    private readonly ILogger<GetData> _logger;

    public GetData(ILogger<GetData> logger)
    {
        _logger = logger;
    }

    [FunctionName("GetData")]
    [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
    [OpenApiRequestBody(MediaTypeNames.Application.Json, typeof(Person))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        Person req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string responseMessage = $"Hello, {req.firstName} {req.lastName}. The time is {DateTime.Now:HH:mm:ss}.";

        return new OkObjectResult(responseMessage);
    }
}

public record Person(string firstName, string lastName) { }
