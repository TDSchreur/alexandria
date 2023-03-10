using Alexandria.Api.Functions;
using Alexandria.Api.Models;
using Alexandria.Api.Security;
using Alexandria.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace Alexandria.Api.UnitTests;

public class GetDataTests
{
    private readonly Mock<ISayHello> _sayHelloMock;
    private readonly GetData _sut;

    public GetDataTests()
    {
        _sayHelloMock = new Mock<ISayHello>(MockBehavior.Strict);
        _sut = new GetData(_sayHelloMock.Object, new NullLogger<GetData>());
    }

    [Fact]
    public void Run_ShouldReturn_Hello()
    {
        _sayHelloMock.Setup(x => x.Hello(It.IsAny<string>(), It.IsAny<string>()))
                     .Returns("Hello, Dennis Schreur. The time is");

        Person p = new("Dennis", "Schreur");
        OkObjectResult okObjectResult = _sut.Run(p, new JwtValidationResult { Status = JwtValidationStatus.Valid }) as OkObjectResult;
        Assert.NotNull(okObjectResult);

        Response response = okObjectResult.Value as Response;
        Assert.NotNull(response);

        Assert.StartsWith("Hello, Dennis Schreur. The time is", response.Message);
    }
}
