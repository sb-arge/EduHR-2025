using EduHR.Application.Interfaces;
using System;

namespace EduHR.Infrastructure.Services;

/// <summary>
/// Implements the IDateTimeService interface.
/// </summary>
public class DateTimeService : IDateTimeService
{
    /// <summary>
    /// Gets the current Coordinated Universal Time (UTC).
    /// </summary>
    public DateTime UtcNow => DateTime.UtcNow;
}