using EduHR.Application.Features.Users.Commands;
using EduHR.Application.Features.Users.Queries;
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

/// <summary>
/// Manages user operations for a tenant.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "TenantAdmin")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new user for the current tenant.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<UserDto>.SuccessResponse(result, "User created successfully."));
    }

    /// <summary>
    /// Retrieves all users for the current tenant.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUsersQuery());
        return Ok(ApiResponse<IEnumerable<UserDto>>.SuccessResponse(result));
    }
    
    /// <summary>
    /// Updates the roles assigned to a user.
    /// </summary>
    [HttpPut("{id}/roles")]
    public async Task<IActionResult> UpdateRoles(int id, [FromBody] UpdateUserRolesCommand command)
    {
        if (id != command.UserId)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        await _mediator.Send(command);
        return Ok(ApiResponse.SuccessResponse("User roles updated successfully."));
    }

    // TODO: Kullanıcı detaylarını getiren (GetById) ve kullanıcıyı pasife alan (Deactivate) endpoint'ler eklenebilir.
}