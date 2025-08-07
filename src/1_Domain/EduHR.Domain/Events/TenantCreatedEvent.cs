using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Yeni bir kiracı oluşturulduğunda yayınlanan olay.
/// </summary>
public class TenantCreatedEvent : BaseEvent
{
    public Tenant Tenant { get; }

    public TenantCreatedEvent(Tenant tenant)
    {
        Tenant = tenant;
    }
}