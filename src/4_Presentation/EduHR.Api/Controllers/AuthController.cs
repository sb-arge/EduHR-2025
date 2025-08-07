using EduHR.Common.DTOs;
using EduHR.Common.DTOs.Auth;
using EduHR.Common.Wrappers;
using EduHR.Domain.Entities;
using EduHR.Infrastructure.Identity; // JwtTokenGenerator için
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(UserManager<User> userManager, JwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="request">The login request containing email and password.</param>
    /// <returns>A JWT token if authentication is successful.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null || !user.IsActive)
        {
            return Unauthorized(ApiResponse.FailResponse("Invalid credentials or user is not active."));
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordCorrect)
        {
            return Unauthorized(ApiResponse.FailResponse("Invalid credentials."));
        }

        var token = await _jwtTokenGenerator.GenerateToken(user);

        var response = new LoginResponseDto
        {
            Token = token,
            UserId = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName
        };

        return Ok(ApiResponse<LoginResponseDto>.SuccessResponse(response, "Login successful."));
    }
}