using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

public class DepartmentDeletedEvent : BaseEvent
{
    public Department Department { get; }
    public DepartmentDeletedEvent(Department department) => Department = department;
}