using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Jobby.HttpApi.Extensions;

public static class AuthenticationExtensions
{
    public static AuthenticationBuilder AddClerkAuthentication(this IServiceCollection services, IConfiguration config)
    {
        return services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            IConfigurationSection clerkConfig = config.GetSection("Clerk");

            string pem = clerkConfig["SignatureKey"] ?? throw new Exception("SignatureKey is missing from Jwt configuration.");
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(pem.ToCharArray());
            SecurityKey issuerSigningKey = new RsaSecurityKey(rsa);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = clerkConfig["Issuer"],
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = issuerSigningKey
            };
        });
    }
}