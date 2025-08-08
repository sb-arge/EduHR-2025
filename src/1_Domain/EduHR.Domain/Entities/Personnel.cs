using EduHR.Domain.Common;
using EduHR.Domain.Enums;
using EduHR.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace EduHR.Domain.Entities;

/// <summary>
/// Represents a personnel record within a tenant.
/// </summary>
public class Personnel : AuditableEntity, ITenantEntity
{
    // ITenantEntity'den gelen zorunluluk
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;

    // Temel Özlük Bilgileri
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Tckn { get; set; } // Türkiye Cumhuriyeti Kimlik Numarası
    public DateTime? DateOfBirth { get; set; }
    public Gender Gender { get; set; }

    // İş Bilgileri
    public DateTime HireDate { get; set; }
    public DateTime? TerminationDate { get; set; }
    public int PositionId { get; set; }
    public Position Position { get; set; } = null!;

    // Kullanıcı Hesabı İlişkisi
    public int? UserId { get; set; }
    public User? User { get; set; }

    // Navigation Properties (İlişkisel Varlıklar)
    public ICollection<PersonnelDocument> Documents { get; set; } = new List<PersonnelDocument>();
    public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();
    public ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    
    // TODO: Diğer ilişkisel varlıklar (eğitim, iş tecrübesi vb.) eklenecek.
}