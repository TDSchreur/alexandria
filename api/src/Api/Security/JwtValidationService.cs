using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Alexandria.Api.Security;

public class JwtValidationService
{
    private const string Authorization = nameof(Authorization);
    private const string Bearer = nameof(Bearer);
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<JwtValidationService> _logger;
    private readonly ISecurityTokenValidator _securityTokenValidator;
    private readonly TokenValidationParameters _tokenValidationParameters;

    public JwtValidationService(IHttpContextAccessor httpContextAccessor,
                                ISecurityTokenValidator securityTokenValidator,
                                JwtConfig jwtConfig,
                                ILogger<JwtValidationService> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _securityTokenValidator = securityTokenValidator;
        _logger = logger;
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.FromSeconds(10),
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKeys = new[] { new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Key)) },
            ValidIssuer = jwtConfig.Issuer
        };
    }

    public JwtValidationResult GetClaimsPrincipalAsync()
    {
        JwtValidationResult result = new() { Status = JwtValidationStatus.Invalid };

        if (!_httpContextAccessor.HttpContext.Request.Headers.TryGetValue(Authorization, out StringValues headerData))
        {
            return result;
        }

        string headerValue = headerData.FirstOrDefault();
        if (headerValue == null)
        {
            return result;
        }

        if (!headerValue.Contains(Bearer, StringComparison.OrdinalIgnoreCase))
        {
            return result;
        }

        string token = headerValue[$"{Bearer} ".Length..];
        if (string.IsNullOrEmpty(token))
        {
            return result;
        }

        try
        {
            ClaimsPrincipal principal = _securityTokenValidator.ValidateToken(token, _tokenValidationParameters, out SecurityToken _);

            return new JwtValidationResult
            {
                Status = JwtValidationStatus.Valid,
                ClaimsPrincipal = principal
            };
        }
        catch (SecurityTokenExpiredException ex)
        {
            _logger.LogWarning(ex, "The token is expired: {Message}", ex.Message);
            return new JwtValidationResult { Status = JwtValidationStatus.Expired };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when validating the user token");
        }

        return result;
    }
}
