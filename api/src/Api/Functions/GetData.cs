using System.Net;
using System.Net.Mime;
using Alexandria.Api.Models;
using Alexandria.Api.Security;
using Alexandria.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Alexandria.Api.Functions;

public class GetData
{
    private readonly ILogger<GetData> _logger;
    private readonly ISayHello _sayHello;

    public GetData(ISayHello sayHello, ILogger<GetData> logger)
    {
        _sayHello = sayHello;
        _logger = logger;
    }

    [FunctionName(nameof(GetData))]
    [OpenApiOperation(nameof(GetData), FunctionCategories.TestFunctions)]
    [OpenApiRequestBody(MediaTypeNames.Application.Json, typeof(Person))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Text.Plain, typeof(string))]
    [OpenApiResponseWithBody(HttpStatusCode.Unauthorized, MediaTypeNames.Application.Json, typeof(JwtValidationResult))]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]
        Person req,
        [Jwt] JwtValidationResult validationResult)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        if (validationResult is not { Status: JwtValidationStatus.Valid })
        {
            return new UnauthorizedObjectResult(validationResult);
        }

        Response response = new() { Message = _sayHello.Hello(req.FirstName, req.LastName) };

        return new OkObjectResult(response);
    }
}
