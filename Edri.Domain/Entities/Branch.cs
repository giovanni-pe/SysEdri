using System;

namespace Edri.Domain.Entities;

public class Branch : Entity
{
    public Guid CompanyId { get; private set; }
    public Guid DistrictId { get; private set; }
    public string Name { get; private set; }
    public string? Address { get; private set; }
    public string? Phone { get; private set; }
    public bool IsActive { get; private set; }

    public virtual Company Company { get; private set; } = null!;
    public virtual District District { get; private set; } = null!;

    public Branch(
        Guid id,
        Guid companyId,
        Guid districtId,
        string name,
        string? address,
        string? phone,
        bool isActive) : base(id)
    {
        CompanyId = companyId;
        DistrictId = districtId;
        Name = name;
        Address = address;
        Phone = phone;
        IsActive = isActive;
    }

    public void Update(
        Guid companyId,
        Guid districtId,
        string name,
        string? address,
        string? phone,
        bool isActive)
    {
        CompanyId = companyId;
        DistrictId = districtId;
        Name = name;
        Address = address;
        Phone = phone;
        IsActive = isActive;
    }
}