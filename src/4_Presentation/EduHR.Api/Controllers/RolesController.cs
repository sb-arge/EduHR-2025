using EduHR.Application.Features.Roles.Commands;
using EduHR.Application.Features.Roles.Queries;
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

/// <summary>
/// Manages tenant-specific role operations.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "TenantAdmin")]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new custom role for the current tenant.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<RoleDto>.SuccessResponse(result, "Role created successfully."));
    }

    /// <summary>
    /// Retrieves all available roles for the current tenant (system roles + custom tenant roles).
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllRolesQuery());
        return Ok(ApiResponse<IEnumerable<RoleDto>>.SuccessResponse(result));
    }
    
    /// <summary>
    /// Updates an existing custom role for the current tenant.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<RoleDto>.SuccessResponse(result, "Role updated successfully."));
    }

    /// <summary>
    /// Deletes a custom role for the current tenant.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteRoleCommand(id));
        return Ok(ApiResponse.SuccessResponse("Role deleted successfully."));
    }
    
    // TODO: Rol detaylarını ve o role atanmış yetkileri (claims) getirecek bir GetById endpoint'i eklenebilir.
    // TODO: Sistemdeki tüm atanabilir yetkilerin (permissions) bir listesini döndüren bir endpoint eklenebilir.
}