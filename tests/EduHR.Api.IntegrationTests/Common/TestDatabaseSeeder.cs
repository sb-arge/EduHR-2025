using EduHR.Domain.Entities;
using EduHR.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EduHR.Api.IntegrationTests.Common;

/// <summary>
/// A utility class to seed the database with initial data for integration tests.
/// </summary>
public static class TestDatabaseSeeder
{
    public static async Task SeedDatabaseAsync(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        // Temel rollerin var olup olmadığını kontrol et
        if (!await roleManager.RoleExistsAsync("Superadmin"))
        {
            await roleManager.CreateAsync(new Role { Name = "Superadmin", Description = "System Administrator" });
            await roleManager.CreateAsync(new Role { Name = "TenantAdmin", Description = "Tenant Administrator" });
            await roleManager.CreateAsync(new Role { Name = "Employee", Description = "Regular Employee" });
        }
        
        // Testler için standart bir Superadmin kullanıcısı oluştur
        var superadminUser = await userManager.FindByEmailAsync("superadmin@eduhr.test");
        if (superadminUser == null)
        {
            superadminUser = new User 
            { 
                UserName = "superadmin@eduhr.test", 
                Email = "superadmin@eduhr.test",
                FirstName = "Super",
                LastName = "Admin",
                IsActive = true,
                // Superadmin bir kiracıya ait olmadığı için TenantId'si 0 veya null olabilir.
                // DbContext'teki global filtreyi aşabilmesi için özel bir durum yönetimi gerekebilir.
                TenantId = 0 
            };
            await userManager.CreateAsync(superadminUser, "Password123!");
            await userManager.AddToRoleAsync(superadminUser, "Superadmin");
        }

        // Testler için standart bir Plan oluştur
        if (!await context.Plans.AnyAsync(p => p.PlanCode == "basic_monthly"))
        {
            var basicPlan = new Plan
            {
                PlanCode = "basic_monthly",
                Price = 50,
                UserLimit = 10,
                BillingCycle = Domain.Enums.BillingCycle.Monthly,
                IsActive = true
            };
            await context.Plans.AddAsync(basicPlan);
            await context.SaveChangesAsync();
        }
    }
}