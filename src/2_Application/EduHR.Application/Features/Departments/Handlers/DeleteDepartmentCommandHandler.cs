using EduHR.Application.Exceptions;
using EduHR.Application.Features.Departments.Commands;
using EduHR.Domain.Entities;
using EduHR.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EduHR.Application.Features.Departments.Handlers;

/// <summary>
/// Handles the DeleteDepartmentCommand.
/// </summary>
public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;

    public DeleteDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
    {
        var departmentToDelete = await _departmentRepository.GetByIdAsync(request.Id);

        if (departmentToDelete is null)
        {
            throw new NotFoundException(nameof(Department), request.Id);
        }

        // --- İŞ KURALI KONTROLÜ ---
        // TODO: Bu departmana bağlı aktif personel veya alt departman olup olmadığını kontrol et.
        // Örneğin: var hasChildren = await _departmentRepository.HasActivePersonnelOrSubDepartmentsAsync(request.Id);
        // if (hasChildren)
        // {
        //     throw new EntityCannotBeDeletedException("Bu departman, içinde aktif personel veya alt departmanlar bulunduğu için silinemez.");
        // }

        _departmentRepository.Delete(departmentToDelete);
        
        // Değişikliklerin kaydedilmesi Unit of Work deseniyle yönetilecektir.
    }
}