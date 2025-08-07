using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Yeni bir kiracıya özel rol oluşturulduğunda yayınlanan olay.
/// </summary>
public class RoleCreatedEvent : BaseEvent
{
    public Role Role { get; }

    public RoleCreatedEvent(Role role)
    {
        Role = role;
    }
}