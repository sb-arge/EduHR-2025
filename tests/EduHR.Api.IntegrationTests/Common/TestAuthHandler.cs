using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EduHR.Api.IntegrationTests.Common;

/// <summary>
/// A custom authentication handler for integration tests to bypass real authentication.
/// </summary>
public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string TestAuthenticationScheme = "Test";

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Testler için sahte (fake) bir kullanıcı kimliği oluşturuyoruz.
        // Bu kimliğe, test senaryosuna göre "Superadmin" rolü veya "tenantId" gibi claim'ler eklenebilir.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "TestUser"),
            new Claim("uid", "1"), // Varsayılan kullanıcı ID'si
            new Claim("tenantId", "1"), // Varsayılan kiracı ID'si
            new Claim(ClaimTypes.Role, "Superadmin") // Varsayılan rol
        };
        
        var identity = new ClaimsIdentity(claims, TestAuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, TestAuthenticationScheme);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}