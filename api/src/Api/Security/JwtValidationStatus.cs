using System.Runtime.Serialization;

namespace Alexandria.Api.Security;

public enum JwtValidationStatus
{
    [EnumMember(Value = "Valid")]
    Valid = 0,
    [EnumMember(Value = "Invalid")]
    Invalid = 1,
    [EnumMember(Value = "Expired")]
    Expired = 2
}
