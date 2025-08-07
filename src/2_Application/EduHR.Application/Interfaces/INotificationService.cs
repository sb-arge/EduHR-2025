using System.Threading.Tasks;
using EduHR.Common.DTOs; // NotificationDto için

namespace EduHR.Application.Interfaces;

/// <summary>
/// Defines the contract for a notification service that can send notifications through various channels.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Sends a notification based on the provided data.
    /// </summary>
    /// <param name="notification">The notification data transfer object.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendNotificationAsync(NotificationDto notification);
}