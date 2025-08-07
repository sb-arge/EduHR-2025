using EduHR.Api.IntegrationTests.Common;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class PositionsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PositionsControllerTests(CustomWebApplicationFactory factory)
    {
        // Bu testler için "TenantAdmin" rolü ve "1" TenantId'si ile sahte bir kimlik oluşturuyoruz.
        _client = factory.CreateClientWithTestAuth(role: "TenantAdmin", tenantId: "1");
    }

    [Fact]
    public async Task GetAll_WithTenantAdminRole_ShouldReturnSuccess()
    {
        // Arrange
        // Client, TenantAdmin rolüyle yapılandırıldı.

        // Act
        var response = await _client.GetAsync("/api/v1/positions");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetAll_WithoutToken_ShouldReturnUnauthorized()
    {
        // Arrange
        var unauthenticatedClient = new CustomWebApplicationFactory().CreateClient();

        // Act
        var response = await unauthenticatedClient.GetAsync("/api/v1/positions");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}