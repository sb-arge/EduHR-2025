using EduHR.Domain.Common;
using System.Collections.Generic;

namespace EduHR.Domain.Events;

public class UserRolesChangedEvent : BaseEvent
{
    public int UserId { get; }
    public IEnumerable<string> AssignedRoles { get; }

    public UserRolesChangedEvent(int userId, IEnumerable<string> assignedRoles)
    {
        UserId = userId;
        AssignedRoles = assignedRoles;
    }
}