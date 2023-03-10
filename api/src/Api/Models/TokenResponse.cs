namespace Alexandria.Api.Models;

public class TokenResponse
{
    public bool Authorized { get; init; }

    public string Token { get; init; }
}
