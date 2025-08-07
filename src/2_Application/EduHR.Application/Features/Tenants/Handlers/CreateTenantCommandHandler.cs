using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Tenants.Commands;
using EduHR.Application.Interfaces;
using EduHR.Domain.Entities;
using EduHR.Domain.Events;
using EduHR.Domain.Exceptions;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Tenants.Handlers;

/// <summary>
/// Handles the CreateTenantCommand to create a new tenant, its first admin user, and initial subscription.
/// </summary>
public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, CreateTenantCommandResponse>
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateTenantCommandHandler(
        ITenantRepository tenantRepository,
        IPlanRepository planRepository,
        ISubscriptionRepository subscriptionRepository,
        IIdentityService identityService,
        IMapper mapper,
        IMediator mediator)
    {
        _tenantRepository = tenantRepository;
        _planRepository = planRepository;
        _subscriptionRepository = subscriptionRepository;
        _identityService = identityService;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateTenantCommandResponse> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
    {
        // --- 1. İş Kuralı Doğrulamaları ---

        // Subdomain'in benzersiz olduğunu kontrol et
        var existingTenant = await _tenantRepository.GetBySubdomainAsync(request.Subdomain);
        if (existingTenant is not null)
        {
            throw DuplicateEntityException.ForEntity("Tenant", "Subdomain", request.Subdomain);
        }

        // Seçilen Plan'ın geçerli olup olmadığını kontrol et
        var plan = await _planRepository.GetByIdAsync(request.PlanId);
        if (plan is null)
        {
            throw new NotFoundException(nameof(Plan), request.PlanId);
        }

        // --- 2. Varlıkları Oluşturma ---
        
        // Bu işlemlerin tamamının tek bir veritabanı işleminde (transaction) gerçekleşmesi gerekir.
        // Bu genellikle Infrastructure katmanında bir UnitOfWork deseni veya Application katmanında bir TransactionBehaviour ile sağlanır.

        // 2a. Yeni Kiracıyı Oluştur
        var newTenant = new Tenant 
        { 
            Name = request.CompanyName, 
            Subdomain = request.Subdomain 
        };
        await _tenantRepository.AddAsync(newTenant);
        // Not: Unit of Work deseni kullanılmadığı varsayılarak, ID'nin oluşması için SaveChanges'in çağrılması gerekebilir.

        // 2b. İlk Yönetici Kullanıcısını Oluştur ve "TenantAdmin" rolünü ata
        var (result, userId) = await _identityService.CreateUserAsync(
            newTenant.Id, 
            request.AdminFirstName, 
            request.AdminLastName, 
            request.AdminEmail, 
            request.AdminPassword,
            new List<string> { "TenantAdmin" }); // Rolü burada sabit olarak belirtiyoruz.
        
        if (!result.Success)
        {
            // Eğer kullanıcı oluşturma başarısız olursa, işlemi durdur ve hatayı fırlat.
            // Transaction sayesinde, oluşturulan Tenant kaydı da geri alınacaktır.
            throw new Exception($"Admin user could not be created: {result.Message}"); 
        }

        // 2c. İlk Aboneliği Oluştur
        var subscription = new Subscription
        {
            TenantId = newTenant.Id,
            PlanId = plan.Id,
            StartDate = DateTime.UtcNow,
            EndDate = plan.BillingCycle == Domain.Enums.BillingCycle.Monthly 
                        ? DateTime.UtcNow.AddMonths(1) 
                        : DateTime.UtcNow.AddYears(1),
            Status = Domain.Enums.SubscriptionStatus.Active,
            PriceAtTimeOfSubscription = plan.Price,
            BillingCycleAtTimeOfSubscription = plan.BillingCycle
        };
        await _subscriptionRepository.AddAsync(subscription);

        // --- 3. Olayı Yayınlama ---
        await _mediator.Publish(new TenantCreatedEvent(newTenant), cancellationToken);

        // --- 4. Yanıtı Döndürme ---
        return new CreateTenantCommandResponse
        {
            TenantId = newTenant.Id,
            AdminUserId = userId,
            CompanyName = newTenant.Name
        };
    }
}