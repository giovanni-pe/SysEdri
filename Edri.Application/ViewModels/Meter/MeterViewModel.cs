using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Meters;

public sealed class MeterViewModel
{
    public Guid Id { get; set; }
    public string MeterNumber { get; set; } = string.Empty;
    public Guid SupplyId { get; set; }
    public string? Brand { get; set; }
    public string? Model { get; set; }
    public int Type { get; set; }
    public string TypeDescription { get; set; } = string.Empty;
    public int? AmperageCapacity { get; set; }
    public DateTime InstallationDate { get; set; }
    public DateTime? LastCalibrationDate { get; set; }
    public int Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;

    public static MeterViewModel FromMeter(Meter meter)
    {
        return new MeterViewModel
        {
            Id = meter.Id,
            MeterNumber = meter.MeterNumber,
            SupplyId = meter.SupplyId,
            Brand = meter.Brand,
            Model = meter.Model,
            Type = meter.Type,
            TypeDescription = meter.Type switch
            {
                1 => "Analog",
                2 => "Digital",
                3 => "Smart",
                _ => "Unknown"
            },
            AmperageCapacity = meter.AmperageCapacity,
            InstallationDate = meter.InstallationDate,
            LastCalibrationDate = meter.LastCalibrationDate,
            Status = meter.Status,
            StatusDescription = meter.Status switch
            {
                1 => "Active",
                2 => "Retired",
                3 => "Damaged",
                _ => "Unknown"
            }
        };
    }
}