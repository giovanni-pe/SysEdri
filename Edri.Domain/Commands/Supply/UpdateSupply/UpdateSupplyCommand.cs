using System;

namespace Edri.Domain.Commands.Supplies.UpdateSupply;

public sealed class UpdateSupplyCommand : CommandBase
{
    private static readonly UpdateSupplyCommandValidation s_validation = new();

    public string SupplyNumber { get; }
    public Guid CustomerId { get; }
    public Guid TariffId { get; }
    public Guid BranchId { get; }
    public Guid DistrictId { get; }
    public string InstallationAddress { get; }
    public string? Reference { get; }
    public decimal? Latitude { get; }
    public decimal? Longitude { get; }
    public string Status { get; }
    public DateTime ActivationDate { get; }
    public DateTime? TerminationDate { get; }

    public UpdateSupplyCommand(
        Guid supplyId,
        string supplyNumber,
        Guid customerId,
        Guid tariffId,
        Guid branchId,
        Guid districtId,
        string installationAddress,
        string? reference,
        decimal? latitude,
        decimal? longitude,
        string status,
        DateTime activationDate,
        DateTime? terminationDate) : base(supplyId)
    {
        SupplyNumber = supplyNumber;
        CustomerId = customerId;
        TariffId = tariffId;
        BranchId = branchId;
        DistrictId = districtId;
        InstallationAddress = installationAddress;
        Reference = reference;
        Latitude = latitude;
        Longitude = longitude;
        Status = status;
        ActivationDate = activationDate;
        TerminationDate = terminationDate;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}