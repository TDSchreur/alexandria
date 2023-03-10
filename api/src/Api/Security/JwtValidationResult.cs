using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexandria.Api.Security;

public class JwtValidationResult
{
    [JsonIgnore]
    public ClaimsPrincipal ClaimsPrincipal { get; init; }
    [JsonConverter(typeof(StringEnumConverter))]
    public JwtValidationStatus Status { get; init; }
}
