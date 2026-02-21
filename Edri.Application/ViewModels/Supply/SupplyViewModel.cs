using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Supplies;

public sealed class SupplyViewModel
{
    public Guid Id { get; set; }
    public string SupplyNumber { get; set; } = string.Empty;
    public Guid CustomerId { get; set; }
    public Guid TariffId { get; set; }
    public Guid BranchId { get; set; }
    public Guid DistrictId { get; set; }
    public string InstallationAddress { get; set; } = string.Empty;
    public string? Reference { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime ActivationDate { get; set; }
    public DateTime? TerminationDate { get; set; }

    public static SupplyViewModel FromSupply(Supply supply)
    {
        return new SupplyViewModel
        {
            Id = supply.Id,
            SupplyNumber = supply.SupplyNumber,
            CustomerId = supply.CustomerId,
            TariffId = supply.TariffId,
            BranchId = supply.BranchId,
            DistrictId = supply.DistrictId,
            InstallationAddress = supply.InstallationAddress,
            Reference = supply.Reference,
            Latitude = supply.Latitude,
            Longitude = supply.Longitude,
            Status = supply.Status,
            ActivationDate = supply.ActivationDate,
            TerminationDate = supply.TerminationDate
        };
    }
}