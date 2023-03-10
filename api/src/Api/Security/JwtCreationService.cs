using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Alexandria.Api.Security;

public class JwtCreationService
{
    private readonly JwtConfig _jwtConfig;
    private readonly SigningCredentials _signingCredentials;

    public JwtCreationService(JwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;

        byte[] key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
    }

    public string GenerateJwtToken(Dictionary<string, string> claims)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims.Select(x => new Claim(x.Key, x.Value))),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience,
            SigningCredentials = _signingCredentials
        };
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
