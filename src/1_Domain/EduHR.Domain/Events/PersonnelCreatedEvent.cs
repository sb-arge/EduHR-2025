using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Yeni bir personel oluşturulduğunda yayınlanan olay.
/// </summary>
public class PersonnelCreatedEvent : BaseEvent
{
    public Personnel Personnel { get; }

    public PersonnelCreatedEvent(Personnel personnel)
    {
        Personnel = personnel;
    }
}