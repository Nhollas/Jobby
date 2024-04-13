using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Jobby.HttpApi.Tests.Helpers;

public static class JwtHelper
{
    public static string DefaultSecurityKey = "YVlGeptVBTTm4iIGZp9nm3xgWcczjmio";
    
    public static string Generate(
        string userId, 
        DateTime? expires = null, 
        string? issuer = null, 
        string? audience = null,
        string? secret = null)
    {
        List<Claim> claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
        };
        
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: issuer ?? "TestIssuer",
            audience: audience ?? "TestAudience",
            claims: claims,
            expires: expires ?? DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret ?? DefaultSecurityKey)),
                SecurityAlgorithms.HmacSha256Signature)
            );
        
        string? tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}