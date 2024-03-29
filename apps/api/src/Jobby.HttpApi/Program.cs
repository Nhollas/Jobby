using System.Security.Cryptography;
using Jobby.Application;
using Jobby.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddLogging();
builder.Services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(config);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Jobby API",
        Version = "v1"
    });

    c.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
        {
            var jwtConfig = config.GetSection("Jwt");

            string pem = jwtConfig["SignatureKey"] ?? throw new Exception("SignatureKey is missing from Jwt configuration.");
            string[] splitPem = Regex.Matches(pem, ".{1,64}").Select(m => m.Value).ToArray();
            string publicKey = "-----BEGIN PUBLIC KEY-----\n" + string.Join("\n", splitPem) + "\n-----END PUBLIC KEY-----";
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(publicKey);
            SecurityKey issuerSigningKey = new RsaSecurityKey(rsa);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtConfig["Issuer"],
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = issuerSigningKey
            };
        }
    );

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(x =>
    x.AllowAnyHeader()
        .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program;