using EduHR.Application.Interfaces;
using EduHR.Domain.Common;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IDateTimeService _dateTimeService;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        ICurrentUserService currentUserService,
        IDateTimeService dateTimeService) : base(options)
    {
        _currentUserService = currentUserService;
        _dateTimeService = dateTimeService;
    }

    // SaaS ve Organizasyon Varlıkları
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Plan> Plans => Set<Plan>();
    public DbSet<Feature> Features => Set<Feature>();
    public DbSet<Subscription> Subscriptions => Set<Subscription>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Position> Positions => Set<Position>();
    
    // Çeviri Varlıkları
    public DbSet<PlanTranslation> PlanTranslations => Set<PlanTranslation>();
    public DbSet<FeatureTranslation> FeatureTranslations => Set<FeatureTranslation>();
    public DbSet<DepartmentTranslation> DepartmentTranslations => Set<DepartmentTranslation>();
    public DbSet<PositionTranslation> PositionTranslations => Set<PositionTranslation>();
    
    // Diğer tüm varlıklar için DbSet'ler buraya eklenecek
    // public DbSet<Personnel> Personnel => Set<Personnel>();
    // public DbSet<LeaveRequest> LeaveRequests => Set<LeaveRequest>();
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Tüm IEntityTypeConfiguration sınıflarını bu assembly içinden bul ve uygula.
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // --- ÇOKLU-KİRACILIK İÇİN GLOBAL FİLTRE (DÜZELTİLMİŞ) ---
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                // 1. Lambda ifadesi için bir parametre oluştur (örn: e => ...)
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                
                // 2. TenantId özelliğine erişim ifadesi oluştur (örn: e.TenantId)
                var property = Expression.Property(parameter, nameof(ITenantEntity.TenantId));
                
                // 3. Karşılaştırılacak olan mevcut kiracının Id'sini bir sabite dönüştür
                var tenantId = Expression.Constant(_currentUserService.TenantId);

                // 4. Eşitlik kontrolü ifadesi oluştur (örn: e.TenantId == _currentUserService.TenantId)
                var body = Expression.Equal(property, tenantId);
                
                // 5. Oluşturulan parçaları bir lambda ifadesinde birleştir
                var lambda = Expression.Lambda(body, parameter);
                
                // 6. Dinamik olarak oluşturulan bu filtreyi uygula
                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserId?.ToString();
                    entry.Entity.CreatedDate = _dateTimeService.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _currentUserService.UserId?.ToString();
                    entry.Entity.UpdatedDate = _dateTimeService.UtcNow;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}