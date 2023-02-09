using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace api;

public static class GetData
{
    [FunctionName("GetData")]
    public static IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
        Person req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string responseMessage = $"Hello, {req.firstName} {req.lastName}. The time is {DateTime.Now:HH:mm:ss}.";

        return new OkObjectResult(responseMessage);
    }
}

public record Person(string firstName, string lastName) { }
