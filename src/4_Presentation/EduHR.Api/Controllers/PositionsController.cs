using EduHR.Application.Features.Organization.Commands; // Varsayımsal - bu command'leri oluşturacağız
using EduHR.Application.Features.Organization.Queries;  // Varsayımsal - bu query'leri oluşturacağız
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

/// <summary>
/// Manages position operations for a tenant.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "TenantAdmin")]
public class PositionsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PositionsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new position for the current tenant.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePositionCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<PositionDto>.SuccessResponse(result, "Position created successfully."));
    }

    /// <summary>
    /// Retrieves all positions for the current tenant, optionally filtered by department.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllPositionsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<IEnumerable<PositionDto>>.SuccessResponse(result));
    }
    
    /// <summary>
    /// Updates an existing position.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePositionCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<PositionDto>.SuccessResponse(result, "Position updated successfully."));
    }

    /// <summary>
    /// Deletes a position.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePositionCommand(id));
        return Ok(ApiResponse.SuccessResponse("Position deleted successfully."));
    }
}