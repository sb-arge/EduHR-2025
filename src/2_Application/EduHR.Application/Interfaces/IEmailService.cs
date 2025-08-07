namespace EduHR.Application.Interfaces;

/// <summary>
/// Defines the contract for an email sending service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email asynchronously.
    /// </summary>
    /// <param name="to">The recipient's email address.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The body of the email, which can be HTML.</param>
    /// <returns>A task that represents the asynchronous send operation.</returns>
    Task SendEmailAsync(string to, string subject, string body);
}