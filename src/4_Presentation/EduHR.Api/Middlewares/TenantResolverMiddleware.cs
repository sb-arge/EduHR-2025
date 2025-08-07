// /src/Presentation/EduHR.Api/Middlewares/TenantResolverMiddleware.cs

using EduHR.Application.Features.Tenants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EduHR.Api.Middlewares
{
    /// <summary>
    /// Gelen HTTP isteğinin Hostname'ini analiz ederek ilgili kiracıyı (tenant) çözer
    /// ve bu bilgiyi ICurrentTenant servisine atar.
    /// Bu, uygulamanın çok kiracılı (multi-tenant) yapısının temelini oluşturur.
    /// </summary>
    public class TenantResolverMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TenantResolverMiddleware> _logger;

        public TenantResolverMiddleware(RequestDelegate next, ILogger<TenantResolverMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Middleware'in ana işlem metodu.
        /// </summary>
        /// <param name="context">Mevcut HTTP context'i.</param>
        /// <param name="tenantService">Kiracı bilgilerini getirmek için kullanılan uygulama servisi.</param>
        /// <param name="currentTenant">Mevcut istek için çözümlenen kiracı bilgilerini tutan servis.</param>
        public async Task InvokeAsync(
            HttpContext context,
            ITenantService tenantService,
            ICurrentTenant currentTenant)
        {
            var hostname = context.Request.Host.Host;
            _logger.LogInformation("Resolving tenant for host: {Hostname}", hostname);

            // Hostname'e göre kiracıyı bul
            var tenant = await tenantService.GetByHostnameAsync(hostname);

            if (tenant is null)
            {
                _logger.LogWarning("Tenant not found for host: {Hostname}. Returning 404 Not Found.", hostname);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync("Tenant not found.");
                return;
            }

            if (!tenant.IsActive)
            {
                _logger.LogWarning("Tenant '{TenantName}' ({TenantId}) is inactive. Returning 403 Forbidden.", tenant.Name, tenant.Id);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Tenant is inactive.");
                return;
            }

            _logger.LogInformation("Tenant '{TenantName}' ({TenantId}) resolved successfully.", tenant.Name, tenant.Id);

            // Mevcut isteğin yaşam döngüsü (scoped) için kiracı bilgilerini ayarla
            currentTenant.SetTenant(tenant.Id, tenant.Name);

            // Pipeline'daki bir sonraki middleware'e devam et
            await _next(context);
        }
    }

    /// <summary>
    /// TenantResolverMiddleware'i IApplicationBuilder'a kolayca eklemek için extension metotları.
    /// </summary>
    public static class TenantResolverMiddlewareExtensions
    {
        /// <summary>
        /// Gelen isteklerde kiracı çözümlemesi yapmak için TenantResolverMiddleware'i pipeline'a ekler.
        /// </summary>
        /// <param name="builder">Uygulama pipeline builder'ı.</param>
        /// <returns>IApplicationBuilder.</returns>
        public static IApplicationBuilder UseTenantResolver(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TenantResolverMiddleware>();
        }
    }
}