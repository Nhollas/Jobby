using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Jobby.HttpApi.Tests.Helpers;

public static class JwtHelper
{
    public const string MockTestPemPublicKey = "-----BEGIN PUBLIC KEY-----\nMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCYMDVAYLcV5xIKIpzDfkaIbuOR\ngR/I4StRy27K4sXtMUOVoBEzEhSvuVlC+qZpHm2MUOJgf26pDurUzFiAqegfwW1i\n30iT5AxvyMu4ZfNVeHSM1QIq5MHQBnu8LcPob+RxOJN3qGIknlsoK0h2RFdkOwtT\nrWviogl0jq0/A2k4JQIDAQAB\n-----END PUBLIC KEY-----";
    private const string MockTestPemPrivateKey = "-----BEGIN RSA PRIVATE KEY-----\nMIICXAIBAAKBgQCYMDVAYLcV5xIKIpzDfkaIbuORgR/I4StRy27K4sXtMUOVoBEz\nEhSvuVlC+qZpHm2MUOJgf26pDurUzFiAqegfwW1i30iT5AxvyMu4ZfNVeHSM1QIq\n5MHQBnu8LcPob+RxOJN3qGIknlsoK0h2RFdkOwtTrWviogl0jq0/A2k4JQIDAQAB\nAoGAH9BUEXUmUMnRF5VMuAE5tWOY5t3bSx7m815XdsoZHhBiVHD1p3tsH5/bbQkA\nqJDXzbFK4WlCNM0NghaFmb/q6gXYnkVZpXluTNEpXwhiMYTcgWMc0WnBh6yfXpn1\n1S1LdW/QPg8SGNH3KLcLLlEHtyTsrR7rnZlBFg3L2Zz6woECQQDQ/8rV7XlPODrm\nfUGIbMoAqLrOFRxmhe0j7xU/ahINz6DXUpW+/ZA2Bg5M0Z5RlD0ZxoGWHfuBMLFA\naCTNK/hNAkEAumnHnK3LEge9APxRAwY/ZUvj+Q6bacYP6mpb3NpDwCw7k7/Wo1q3\nKZ9Fs+/BONWvrR6DZIBrolQt1ms7rpIrOQJAEQHPqqrhZT3pJRpqO8TGh1kzolN7\ndShOzM7GxttFztPnfb4dq2YM1yiU/1FLdc3/TtqlbubPsYqZ0ejnnb6HPQJBALXB\nxdtCqSmfWe3IFsU4JkACSvngADCV6ZbKtV8VgdGyTzS0a/dC0CxJG5FiR6e+0led\n1Mb9X/Ua1samL776ziECQHtvvjtPF+8ETR77RC7wI9pgBE5C1jFzY1oWep+ncuXa\nejwHlzvsp3HT/hk75nJUPLT88Yx3AgmMT/utHsUwcik=\n-----END RSA PRIVATE KEY-----";

    public static string Generate(
        string userId, 
        DateTime? expires = null, 
        string? issuer = null, 
        string? privatePemKey = null)
    {
        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, userId)
        ];

        RSA rsa = RSA.Create();
        var pem = privatePemKey?.ToCharArray() ?? MockTestPemPrivateKey.ToCharArray();
        rsa.ImportFromPem( pem);
        SecurityKey issuerSigningKey = new RsaSecurityKey(rsa);
        
        JwtSecurityToken token = new(
            issuer: issuer ?? "TestIssuer",
            claims: claims,
            expires: expires ?? DateTime.Now.AddDays(1),
            signingCredentials: new SigningCredentials(issuerSigningKey , SecurityAlgorithms.RsaSha256)
        );
        
        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenString;
    }
}