namespace EduHR.Application.Interfaces;

/// <summary>
/// Provides information about the current user making the request.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// The ID of the currently authenticated user.
    /// Can be null if the request is anonymous.
    /// </summary>
    int? UserId { get; }

    /// <summary>
    /// The Tenant ID of the currently authenticated user.
    /// Can be null if the user is a Superadmin or the request is anonymous.
    /// </summary>
    int? TenantId { get; }

    /// <summary>
    /// The roles of the currently authenticated user.
    /// </summary>
    IReadOnlyList<string> Roles { get; }

    /// <summary>
    /// The claims of the currently authenticated user.
    /// </summary>
    IReadOnlyDictionary<string, string> Claims { get; }
}