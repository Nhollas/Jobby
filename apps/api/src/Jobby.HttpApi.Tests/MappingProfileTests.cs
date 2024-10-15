using Jobby.Application;

namespace Jobby.HttpApi.Tests;

using AutoMapper;
using Xunit;

public class MappingProfileTests
{
    [Fact]
    public void MappingProfile_ConfigurationIsValid()
    {
        MapperConfiguration configuration = new(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        configuration.AssertConfigurationIsValid();
    }
}