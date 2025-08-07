using EduHR.Api.IntegrationTests.Common;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class DepartmentsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public DepartmentsControllerTests(CustomWebApplicationFactory factory)
    {
        // Bu testler için "TenantAdmin" rolüyle sahte bir kimlik oluşturmamız gerekir.
        _client = factory.CreateClientWithTestAuth(role: "TenantAdmin", tenantId: "1");
    }
    
    [Fact]
    public async Task GetAll_WithTenantAdminRole_ShouldReturnSuccess()
    {
        // Arrange
        // Client, TenantAdmin rolüyle yapılandırıldı.

        // Act
        var response = await _client.GetAsync("/api/v1/departments");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}