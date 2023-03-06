using api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace apitests;

public class GetDataTests
{
    private readonly GetData _sut;

    public GetDataTests()
    {
        _sut = new GetData(new NullLogger<GetData>());
    }

    [Fact]
    public void Run_ShouldReturn_Hello()
    {
        Person p = new("Dennis", "Schreur");
        OkObjectResult okObjectResult = _sut.Run(p) as OkObjectResult;
        Assert.NotNull(okObjectResult);

        Response response = okObjectResult.Value as Response;
        Assert.NotNull(response);

        Assert.StartsWith("Hello, Dennis Schreur. The time is", response.Message);
    }
}
