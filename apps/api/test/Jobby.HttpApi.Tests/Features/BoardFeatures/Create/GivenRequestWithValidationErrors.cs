using System.Net;
using System.Net.Http.Json;
using System.Text;
using Jobby.HttpApi.Tests.Factories;

namespace Jobby.HttpApi.Tests.Features.BoardFeatures.Create;

[Collection("SqlCollection")]
public class GivenRequestWithValidationErrors(JobbyHttpApiFactory factory)
{
    private HttpClient HttpClient => factory.SetupClient();
    
    [Fact]
    public async Task WhenNamePropertyIsMissing_ThenReturns422UnprocessableEntityAndNamePropertyValidationMessage()
    {
        const string withoutNameProperty = "{}";

        StringContent body = new(withoutNameProperty, Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await HttpClient.PostAsync("/board", body);
        
        var errors = await response.Content.ReadFromJsonAsync<List<ValidationError>>();

        using (new AssertionScope())
        {
            errors.Should().NotBeNull();
            errors![0].Should().NotBeNull();
            errors[0].PropertyName.Should().Be("Name");
            errors[0].Message.Should().Be("Name is required.");
            
            response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
        }
    }
}