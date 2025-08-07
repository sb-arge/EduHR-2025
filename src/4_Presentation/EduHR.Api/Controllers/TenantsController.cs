using EduHR.Application.Features.Tenants.Commands;
using EduHR.Application.Features.Tenants.Queries; // <-- Artık geçerli bir using
using EduHR.Common.DTOs;
using EduHR.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EduHR.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(Roles = "Superadmin")]
public class TenantsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TenantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTenantCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(ApiResponse<CreateTenantCommandResponse>.SuccessResponse(response, "Tenant created successfully."));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllTenantsQuery query)
    {
        var result = await _mediator.Send(query);
        return Ok(ApiResponse<PagedResultDto<TenantDto>>.SuccessResponse(result));
    }
}