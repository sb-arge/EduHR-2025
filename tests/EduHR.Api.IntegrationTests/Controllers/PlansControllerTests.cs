using EduHR.Api.IntegrationTests.Common;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class PlansControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PlansControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClientWithTestAuth(); // TestAuthHandler'ı kullanan client
    }
    
    [Fact]
    public async Task GetAll_WithSuperadminRole_ShouldReturnSuccess()
    {
        // Arrange
        // TestAuthHandler varsayılan olarak Superadmin rolünü sağlıyor.

        // Act
        var response = await _client.GetAsync("/api/v1/plans");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}