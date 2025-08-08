using EduHR.Common.DTOs;
using MediatR;
using System;

namespace EduHR.Application.Features.Personnel.Commands;

/// <summary>
/// Yeni bir personel kaydı ve ilişkili kullanıcı hesabını oluşturmak için komutu temsil eder.
/// </summary>
public class CreatePersonnelCommand : IRequest<PersonnelSummaryDto>
{
    // Temel Özlük Bilgileri
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Tckn { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }

    // İş Bilgileri
    public DateTime HireDate { get; set; }
    public int PositionId { get; set; }

    // Kullanıcı Hesabı Bilgileri
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
}