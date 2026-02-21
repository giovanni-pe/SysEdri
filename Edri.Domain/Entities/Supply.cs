using System;

namespace Edri.Domain.Entities;

public class Supply : Entity
{
    public string SupplyNumber { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid TariffId { get; private set; }
    public Guid BranchId { get; private set; }
    public Guid DistrictId { get; private set; }
    public string InstallationAddress { get; private set; }
    public string? Reference { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public string Status { get; private set; }
    public DateTime ActivationDate { get; private set; }
    public DateTime? TerminationDate { get; private set; }

    public virtual Customer Customer { get; private set; } = null!;
    public virtual Tariff Tariff { get; private set; } = null!;
    public virtual Branch Branch { get; private set; } = null!;
    public virtual District District { get; private set; } = null!;

    public Supply(
        Guid id,
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
        DateTime? terminationDate) : base(id)
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

    public void Update(
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
        DateTime? terminationDate)
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
}