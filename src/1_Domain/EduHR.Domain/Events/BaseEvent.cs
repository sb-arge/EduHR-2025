using MediatR; // Bu satırın ekli olduğundan emin olun.
namespace EduHR.Domain.Common;

/// <summary>
/// Domain içinde meydana gelen bir olayı temsil eden temel sınıf.
/// Tüm domain olayları bu sınıftan türemelidir.
/// </summary>
public abstract class BaseEvent : INotification
{
}