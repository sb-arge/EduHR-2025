using MediatR;

namespace EduHR.Application.Features.Departments.Commands;

/// <summary>
/// Mevcut bir departmanı silmek için komutu temsil eder.
/// </summary>
public class DeleteDepartmentCommand : IRequest
{
    public int Id { get; set; }

    public DeleteDepartmentCommand(int id)
    {
        Id = id;
    }
}