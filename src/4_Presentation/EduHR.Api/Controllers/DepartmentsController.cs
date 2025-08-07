using EduHR.Application.Features.Departments.Commands;
using EduHR.Application.Features.Departments.Queries;
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

/// <summary>
/// Manages department operations for a tenant.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "TenantAdmin")] // Bu Controller'a sadece Kiracı Yöneticileri erişebilir.
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Creates a new department for the current tenant.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<DepartmentDto>.SuccessResponse(result, "Department created successfully."));
    }

    /// <summary>
    /// Retrieves all departments for the current tenant.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllDepartmentsQuery());
        return Ok(ApiResponse<IEnumerable<DepartmentDto>>.SuccessResponse(result));
    }

    /// <summary>
    /// Updates an existing department.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(ApiResponse.FailResponse("ID mismatch."));
        }
        var result = await _mediator.Send(command);
        return Ok(ApiResponse<DepartmentDto>.SuccessResponse(result, "Department updated successfully."));
    }

    /// <summary>
    /// Deletes a department.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteDepartmentCommand(id));
        return Ok(ApiResponse.SuccessResponse("Department deleted successfully."));
    }
}