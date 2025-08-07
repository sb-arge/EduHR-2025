using EduHR.Domain.Common;
using EduHR.Domain.Entities;

namespace EduHR.Domain.Events;

/// <summary>
/// Yeni bir kullanıcı oluşturulduğunda yayınlanan olay.
/// </summary>
public class UserCreatedEvent : BaseEvent
{
    public User User { get; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}