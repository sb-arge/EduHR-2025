using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace EduHR.Api.IntegrationTests.Common;

/// <summary>
/// A custom factory for bootstrapping the application in memory for integration tests.
/// </summary>
public class CustomWebApplicationFactory : WebApplicationFactory<Program> // "Program" API projesinin başlangıç sınıfına referanstır.
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Mevcut DbContext kaydını bul ve kaldır.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Testler için In-Memory Database (bellek içi veritabanı) ekle.
            // Alternatif olarak, Testcontainers ile geçici bir PostgreSQL veritabanı da burada yapılandırılabilir.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("EduHR_TestDb");
            });

            // TODO: Testler için sahte (mock) IEmailService gibi servisleri de burada kaydedebiliriz.
        });

        builder.UseEnvironment("Test");
    }
}