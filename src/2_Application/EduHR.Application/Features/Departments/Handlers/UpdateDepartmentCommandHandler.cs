using AutoMapper;
using EduHR.Application.Exceptions;
using EduHR.Application.Features.Departments.Commands;
using EduHR.Common.DTOs;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Departments.Handlers;

/// <summary>
/// Handles the UpdateDepartmentCommand.
/// </summary>
public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public UpdateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentToUpdate = await _departmentRepository.GetByIdAsync(request.Id);

        if (departmentToUpdate is null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        // AutoMapper, request'teki verileri mevcut departmentToUpdate nesnesinin üzerine yazar.
        _mapper.Map(request, departmentToUpdate);

        _departmentRepository.Update(departmentToUpdate);
        
        // Değişikliklerin kaydedilmesi Unit of Work deseniyle yönetilecektir.

        return _mapper.Map<DepartmentDto>(departmentToUpdate);
    }
}