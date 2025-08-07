using EduHR.Application.Interfaces;
using EduHR.Domain.Interfaces;
using EduHR.Infrastructure.Identity;
using EduHR.Infrastructure.Persistence.Contexts;
using EduHR.Infrastructure.Persistence.Repositories;
using EduHR.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EduHR.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext'i PostgreSQL bağlantısıyla kaydet
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        // Repository'leri kaydet
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        // ... diğer tüm repository'ler buraya eklenecek ...

        // Dış Servisleri kaydet
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        // ... diğer tüm servisler buraya eklenecek ...
        
        return services;
    }
}