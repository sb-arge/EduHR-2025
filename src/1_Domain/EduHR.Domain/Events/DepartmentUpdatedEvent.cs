using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

public class DepartmentUpdatedEvent : BaseEvent
{
    public Department Department { get; }
    public DepartmentUpdatedEvent(Department department) => Department = department;
}