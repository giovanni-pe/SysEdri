using System;

namespace Edri.Domain.Entities;

public class Customer : Entity
{
    public Guid UserId { get; private set; }
    public Guid DistrictId { get; private set; }
    public string DocumentType { get; private set; }
    public string DocumentNumber { get; private set; }
    public string CustomerType { get; private set; }
    public string? BusinessName { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? Address { get; private set; }
    public string? Phone { get; private set; }
    public string? Email { get; private set; }
    public string Status { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public virtual User User { get; private set; } = null!;
    public virtual District District { get; private set; } = null!;

    public Customer(
        Guid id,
        Guid userId,
        Guid districtId,
        string documentType,
        string documentNumber,
        string customerType,
        string? businessName,
        string? firstName,
        string? lastName,
        string? address,
        string? phone,
        string? email,
        string status) : base(id)
    {
        UserId = userId;
        DistrictId = districtId;
        DocumentType = documentType;
        DocumentNumber = documentNumber;
        CustomerType = customerType;
        BusinessName = businessName;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Phone = phone;
        Email = email;
        Status = status;
        RegistrationDate = DateTime.UtcNow;
    }

    public void Update(
        Guid userId,
        Guid districtId,
        string documentType,
        string documentNumber,
        string customerType,
        string? businessName,
        string? firstName,
        string? lastName,
        string? address,
        string? phone,
        string? email,
        string status)
    {
        UserId = userId;
        DistrictId = districtId;
        DocumentType = documentType;
        DocumentNumber = documentNumber;
        CustomerType = customerType;
        BusinessName = businessName;
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        Phone = phone;
        Email = email;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}