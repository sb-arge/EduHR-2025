using EduHR.Api.IntegrationTests.Common;
using EduHR.Common.DTOs.Auth;
using EduHR.Common.Wrappers;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithValidCredentials_ShouldReturnSuccessAndToken()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Email = "superadmin@eduhr.test", // TestDatabaseSeeder'da oluşturduğumuz kullanıcı
            Password = "Password123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();
        apiResponse.Should().NotBeNull();
        apiResponse!.Success.Should().BeTrue();
        apiResponse.Data.Should().NotBeNull();
        apiResponse.Data!.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
    {
        // Arrange
        var request = new LoginRequestDto
        {
            Email = "superadmin@eduhr.test",
            Password = "WrongPassword!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}