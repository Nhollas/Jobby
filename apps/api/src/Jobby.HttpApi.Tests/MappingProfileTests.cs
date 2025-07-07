using Jobby.Application;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace Jobby.HttpApi.Tests;

public class MappingProfileTests
{
    [Fact]
    public void MappingProfile_ConfigurationIsValid()
    {
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }, new LoggerFactory());

        configuration.AssertConfigurationIsValid();
    }
}