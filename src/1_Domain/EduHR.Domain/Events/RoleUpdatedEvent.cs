using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Mevcut bir rol güncellendiğinde yayınlanan olay.
/// </summary>
public class RoleUpdatedEvent : BaseEvent
{
    public Role Role { get; }

    public RoleUpdatedEvent(Role role)
    {
        Role = role;
    }
}