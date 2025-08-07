using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using EduHR.Application.Exceptions; // Varsayımsal: Kendi özel Exception sınıflarınızın namespace'i
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EduHR.Api.Middlewares
{
    /// <summary>
    /// Handles exceptions globally for the application, creating a standardized RFC 7807 problem details response.
    /// </summary>
    public sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="logger">The logger instance for logging exceptions.</param>
        /// <param name="env">The hosting environment to determine if detailed errors should be shown.</param>
        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handles the exception and writes a problem details response to the client.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception that was thrown.</param>
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = CreateProblemDetails(context, exception);

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status ?? (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(problemDetails, options);

            return context.Response.WriteAsync(json);
        }

        /// <summary>
        /// Creates a ProblemDetails object based on the exception type.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="exception">The exception.</param>
        /// <returns>A populated ProblemDetails object.</returns>
        private ProblemDetails CreateProblemDetails(HttpContext context, Exception exception)
        {
            var problemDetails = new ProblemDetails
            {
                Instance = context.Request.Path,
                Extensions = { { "traceId", context.TraceIdentifier } }
            };

            switch (exception)
            {
                case ValidationException validationException:
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    problemDetails.Title = "One or more validation errors occurred.";
                    problemDetails.Status = (int)HttpStatusCode.BadRequest;
                    problemDetails.Extensions["errors"] = validationException.Errors;
                    break;

                case NotFoundException notFoundException:
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                    problemDetails.Title = "The specified resource was not found.";
                    problemDetails.Status = (int)HttpStatusCode.NotFound;
                    problemDetails.Detail = notFoundException.Message;
                    break;
                    
                case UnauthorizedAccessException unauthorizedAccessException:
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7235#section-3.1";
                    problemDetails.Title = "Unauthorized access.";
                    problemDetails.Status = (int)HttpStatusCode.Unauthorized;
                    problemDetails.Detail = unauthorizedAccessException.Message;
                    break;

                // Gelecekte eklenebilecek diğer özel istisna türleri buraya eklenebilir.
                // case ForbiddenAccessException forbiddenAccessException:
                //     ...

                default:
                    problemDetails.Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
                    problemDetails.Title = "An internal server error occurred.";
                    problemDetails.Status = (int)HttpStatusCode.InternalServerError;
                    // Geliştirme ortamında daha detaylı bilgi ver, production'da gizle.
                    problemDetails.Detail = _env.IsDevelopment()
                        ? exception.ToString()
                        : "An unexpected error occurred. Please try again later.";
                    break;
            }

            return problemDetails;
        }
    }

    /// <summary>
    /// Extension methods for adding the <see cref="ExceptionHandlerMiddleware"/> to the pipeline.
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Adds the custom exception handler middleware to the application's request pipeline.
        /// </summary>
        /// <param name="builder">The IApplicationBuilder instance.</param>
        /// <returns>The IApplicationBuilder instance for chaining.</returns>
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}