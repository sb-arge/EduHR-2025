using EduHR.Api.IntegrationTests.Common;
using EduHR.Common.DTOs.Auth;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers; // AuthenticationHeaderValue için eklendi
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace EduHR.Api.IntegrationTests.Controllers;

public class TenantsControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TenantsControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    private async Task<string> GetSuperadminTokenAsync()
    {
        var request = new LoginRequestDto
        {
            Email = "superadmin@eduhr.test", // TestDatabaseSeeder'da oluşturduğumuz kullanıcı
            Password = "Password123!"
        };
        var response = await _client.PostAsJsonAsync("/api/v1/auth/login", request);
        var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<LoginResponseDto>>();
        return apiResponse!.Data!.Token;
    }

    [Fact]
    public async Task GetAll_WithoutToken_ShouldReturnUnauthorized()
    {
        // Arrange
        // Token'ı client'a eklemiyoruz.

        // Act
        var response = await _client.GetAsync("/api/v1/tenants");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetAll_WithSuperadminToken_ShouldReturnSuccess()
    {
        // Arrange
        // Superadmin olarak giriş yapıp token'ı alıyoruz.
        var token = await GetSuperadminTokenAsync();
        // Aldığımız token'ı sonraki isteklerin başlığına (header) ekliyoruz.
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var response = await _client.GetAsync("/api/v1/tenants");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}