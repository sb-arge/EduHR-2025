using EduHR.Api.IntegrationTests.Common;
using EduHR.Application.Features.Users.Commands;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public UsersControllerTests(CustomWebApplicationFactory factory)
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
        var response = await _client.GetAsync("/api/v1/users");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_WithValidData_ShouldReturnSuccess()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            FirstName = "Test",
            LastName = "User",
            Email = $"testuser_{System.Guid.NewGuid()}@eduhr.test", // Her seferinde benzersiz email
            Password = "Password123!",
            Roles = new List<string> { "Employee" }
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/users", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}