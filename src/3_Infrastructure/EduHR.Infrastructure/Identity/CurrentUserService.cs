using EduHR.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EduHR.Infrastructure.Identity;

/// <summary>
/// Implements ICurrentUserService by accessing the HttpContext.
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            return userIdClaim != null ? int.Parse(userIdClaim) : null;
        }
    }

    public int? TenantId
    {
        get
        {
            var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue("tenantId");
            return tenantIdClaim != null ? int.Parse(tenantIdClaim) : null;
        }
    }

    public IReadOnlyList<string> Roles => _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList() ?? new List<string>();

    public IReadOnlyDictionary<string, string> Claims => _httpContextAccessor.HttpContext?.User?.Claims
                                .ToDictionary(c => c.Type, c => c.Value) ?? new Dictionary<string, string>();
}