using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Users.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Events;
using EduHR.Domain.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Users.Handlers;

/// <summary>
/// Handles the CreateUserCommand.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateUserCommandHandler(
        IIdentityService identityService,
        ICurrentUserService currentUserService,
        IUserRepository userRepository,
        IMapper mapper,
        IMediator mediator)
    {
        _identityService = identityService;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined for user creation.");

        // IIdentityService'i kullanarak kullanıcıyı ve rollerini oluştur.
        var (result, userId) = await _identityService.CreateUserAsync(
            tenantId, 
            request.FirstName, 
            request.LastName, 
            request.Email, 
            request.Password,
            request.Roles);
            
        if (!result.Success)
        {
            // Identity servisinden gelen bir hata varsa, işlemi durdur.
            // Bu hata genellikle "duplicate email" gibi bir validasyon hatasıdır.
            throw new ValidationException(result.Message);
        }

        // Event'i yayınlamak ve DTO döndürmek için yeni oluşturulan User varlığını al.
        var newUser = await _userRepository.GetByIdAsync(userId);
        if (newUser is null)
        {
            // Bu durumun gerçekleşmesi pek olası değildir, ancak bir güvenlik önlemidir.
            throw new NotFoundException(nameof(User), userId);
        }

        // Diğer sistemleri haberdar etmek için olayı yayınla.
        await _mediator.Publish(new UserCreatedEvent(newUser), cancellationToken);
        
        // Sonucu DTO olarak geri dön.
        return _mapper.Map<UserDto>(newUser);
    }
}