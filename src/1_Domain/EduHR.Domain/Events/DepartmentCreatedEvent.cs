using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Yeni bir departman oluşturulduğunda yayınlanan olay.
/// </summary>
public class DepartmentCreatedEvent : BaseEvent
{
    public Department Department { get; }

    public DepartmentCreatedEvent(Department department)
    {
        Department = department;
    }
}