namespace Alexandria.Api;

public class JwtConfig
{
    public string Audience { get; init; }

    public string Issuer { get; init; }

    public string Key { get; init; }
}
