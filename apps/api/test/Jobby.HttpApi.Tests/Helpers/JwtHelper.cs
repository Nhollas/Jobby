using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Jobby.HttpApi.Tests.Helpers;

public static class JwtHelper
{
    public static string Generate(string userId)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
        };
        
        var token = new JwtSecurityToken(
            issuer: "TestIssuer",
            audience: "TestAudience",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9f7b309b-1dcc-4a96-a292-dbe6e830d8c3")),
                SecurityAlgorithms.HmacSha256Signature)
            );
        
        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}