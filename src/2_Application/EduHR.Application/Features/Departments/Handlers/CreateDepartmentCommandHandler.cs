using AutoMapper;
using EduHR.Application.Features.Departments.Commands;
using EduHR.Application.Interfaces;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Events;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Departments.Handlers;

/// <summary>
/// Handles the CreateDepartmentCommand.
/// </summary>
public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public CreateDepartmentCommandHandler(
        IDepartmentRepository departmentRepository, 
        IMapper mapper, 
        IMediator mediator,
        ICurrentUserService currentUserService)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        // AutoMapper kullanarak komutu yeni bir Department varlığına dönüştür
        var newDepartment = _mapper.Map<Department>(request);
        
        // O anki kullanıcının kiracı kimliğini ata (Çoklu-Kiracılık Güvenliği)
        newDepartment.TenantId = _currentUserService.TenantId ?? throw new UnauthorizedAccessException();

        // Veritabanına ekle
        await _departmentRepository.AddAsync(newDepartment);
        
        // Diğer sistemleri haberdar etmek için olayı yayınla
        await _mediator.Publish(new DepartmentCreatedEvent(newDepartment), cancellationToken);
        
        // Sonucu DTO olarak geri dön
        return _mapper.Map<DepartmentDto>(newDepartment);
    }
}