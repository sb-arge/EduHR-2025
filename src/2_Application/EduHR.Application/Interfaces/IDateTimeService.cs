namespace EduHR.Application.Interfaces;

/// <summary>
/// Provides the current date and time, allowing for abstraction and testability.
/// </summary>
public interface IDateTimeService
{
    /// <summary>
    /// Gets the current Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }
}