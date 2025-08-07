using AutoMapper;
using EduHR.Application.Features.Users.Queries;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Users.Handlers;

/// <summary>
/// Handles the GetAllUsersQuery.
/// </summary>
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository,
        IMapper mapper,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // O anki kullanıcının kiracı kimliğini al (Çoklu-Kiracılık Güvenliği)
        var tenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException("Tenant ID could not be determined.");

        // Repository'den sadece o kiracıya ait kullanıcıları çek
        var users = await _userRepository.GetAllByTenantIdAsync(tenantId);
        
        // Sonucu DTO listesine dönüştür ve geri dön
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}