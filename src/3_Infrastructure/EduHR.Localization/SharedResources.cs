namespace EduHR.Infrastructure.Localization;

/// <summary>
/// A marker class for shared localization resources.
/// This class is used by the IStringLocalizer<SharedResources> to find the .resx files
/// containing shared translations (e.g., "Success", "Error", "NotFound", button texts, etc.)
/// that can be used across different layers of the application (API, WebApp, etc.).
/// </summary>
public class SharedResources
{
    // This class is intentionally empty.
    // It's used as a type marker for the localization system.
}