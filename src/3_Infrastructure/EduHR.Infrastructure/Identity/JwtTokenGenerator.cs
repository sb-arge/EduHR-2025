using EduHR.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; // <-- Eklendi
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Identity;

/// <summary>
/// Generates JWT tokens for authenticated users.
/// </summary>
public class JwtTokenGenerator
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            // Null olabilecek alanlar için kontrol ekliyoruz
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("uid", user.Id.ToString()),
            new Claim("tenantId", user.TenantId.ToString())
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        claims.AddRange(userClaims);

        // Gizli anahtarın null olup olmadığını kontrol et
        var secret = _configuration["JwtSettings:Secret"] ?? throw new InvalidOperationException("JWT Secret not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}