using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EduHR.Api.Middlewares;

/// <summary>
/// Sets the request's culture based on query string or headers. This middleware
/// ensures that the correct localization resources are used for each request.
/// </summary>
public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    // Define the cultures your application supports.
    // This can be moved to appsettings.json for more flexibility.
    private static readonly List<string> _supportedCultures = new() { "tr-TR", "en-US" };
    private const string _defaultCulture = "tr-TR";

    /// <summary>
    /// Initializes a new instance of the <see cref="CultureMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the request pipeline.</param>
    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware to process the HTTP request and set the appropriate culture.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <returns>A <see cref="Task"/> that represents the execution of this middleware.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var cultureCode = GetCultureFromRequest(context);
        var cultureInfo = GetValidatedCultureInfo(cultureCode);

        // Set the culture for the current thread.
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        // Call the next middleware in the pipeline.
        await _next(context);
    }

    /// <summary>
    /// Extracts the culture code from the request, checking query string first, then headers.
    /// </summary>
    private static string GetCultureFromRequest(HttpContext context)
    {
        // 1. Try to get culture from query string
        if (context.Request.Query.TryGetValue("culture", out var cultureFromQuery))
        {
            return cultureFromQuery.ToString();
        }

        // 2. Try to get culture from "Accept-Language" header
        if (context.Request.Headers.TryGetValue("Accept-Language", out var acceptLanguageHeader))
        {
            // The header can contain multiple languages, e.g., "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7"
            // We take the first one.
            return acceptLanguageHeader.ToString().Split(',').FirstOrDefault()?.Trim() ?? string.Empty;
        }

        // 3. Fallback to default culture
        return _defaultCulture;
    }

    /// <summary>
    /// Validates the given culture code and returns a supported CultureInfo object.
    /// </summary>
    private static CultureInfo GetValidatedCultureInfo(string cultureCode)
    {
        if (!string.IsNullOrEmpty(cultureCode) && _supportedCultures.Contains(cultureCode, StringComparer.OrdinalIgnoreCase))
        {
            try
            {
                return new CultureInfo(cultureCode);
            }
            catch (CultureNotFoundException)
            {
                // If the culture code is invalid for any reason, fall back to the default.
                return new CultureInfo(_defaultCulture);
            }
        }
        
        // If the requested culture is not supported or empty, fall back to the default.
        return new CultureInfo(_defaultCulture);
    }
}

/// <summary>
/// Extension methods for adding the <see cref="CultureMiddleware"/> to the application's request pipeline.
/// </summary>
public static class CultureMiddlewareExtensions
{
    /// <summary>
    /// Adds the <see cref="CultureMiddleware"/> to the specified <see cref="IApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> so that additional calls can be chained.</returns>
    public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CultureMiddleware>();
    }
}