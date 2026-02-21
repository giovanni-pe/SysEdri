using System;

namespace Edri.Domain.Commands.Branches.UpdateBranch;

public sealed class UpdateBranchCommand : CommandBase
{
    private static readonly UpdateBranchCommandValidation s_validation = new();

    public Guid CompanyId { get; }
    public Guid DistrictId { get; }
    public string Name { get; }
    public string? Address { get; }
    public string? Phone { get; }
    public bool IsActive { get; }

    public UpdateBranchCommand(
        Guid branchId,
        Guid companyId,
        Guid districtId,
        string name,
        string? address,
        string? phone,
        bool isActive) : base(branchId)
    {
        CompanyId = companyId;
        DistrictId = districtId;
        Name = name;
        Address = address;
        Phone = phone;
        IsActive = isActive;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}