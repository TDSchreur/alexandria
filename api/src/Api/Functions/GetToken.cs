using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using Alexandria.Api.Models;
using Alexandria.Api.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;

namespace Alexandria.Api.Functions;

public class GetToken
{
    private readonly JwtCreationService _jwtCreationService;
    private readonly ILogger<GetToken> _logger;

    public GetToken(JwtCreationService jwtCreationService, ILogger<GetToken> logger)
    {
        _jwtCreationService = jwtCreationService;
        _logger = logger;
    }

    [FunctionName(nameof(GetToken))]
    [OpenApiOperation(nameof(GetToken), FunctionCategories.TestFunctions)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, MediaTypeNames.Application.Json, typeof(TokenResponse), Description = "The OK response")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string token = _jwtCreationService.GenerateJwtToken(new Dictionary<string, string>
        {
            { "name", "Dennis" },
            { "role", "admin" }
        });
        TokenResponse response = new()
        {
            Authorized = true,
            Token = token
        };

        return new OkObjectResult(response);
    }
}
