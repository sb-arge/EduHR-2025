using EduHR.Application.Features.Plans.Commands;
using EduHR.Application.Features.Plans.Queries;
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

/// <summary>
/// Manages subscription plans, accessible only by Superadmins.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "Superadmin")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new subscription plan.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlanCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(ApiResponse<CreatePlanCommandResponse>.SuccessResponse(response, "Plan created successfully."));
    }

    /// <summary>
    /// Retrieves all subscription plans with pagination.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllPlansQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<PagedResultDto<PlanDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Updates an existing subscription plan.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlanCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        await _mediator.Send(command);
        return Ok(ApiResponse.SuccessResponse("Plan updated successfully."));
    }

    /// <summary>
    /// Deletes a subscription plan.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeletePlanCommand(id));
        return Ok(ApiResponse.SuccessResponse("Plan deleted successfully."));
    }

    /// <summary>
    /// Updates the features associated with a plan.
    /// </summary>
    [HttpPut("{id}/features")]
    public async Task<IActionResult> UpdateFeatures(int id, [FromBody] UpdatePlanFeaturesCommand command)
    {
        if (id != command.PlanId)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        await _mediator.Send(command);
        return Ok(ApiResponse.SuccessResponse("Plan features updated successfully."));
    }
}