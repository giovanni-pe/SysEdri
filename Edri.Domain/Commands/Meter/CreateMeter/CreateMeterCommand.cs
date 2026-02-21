using System;

namespace Edri.Domain.Commands.Meters.CreateMeter;

public sealed class CreateMeterCommand : CommandBase
{
    private static readonly CreateMeterCommandValidation s_validation = new();

    public string MeterNumber { get; }
    public Guid SupplyId { get; }
    public string? Brand { get; }
    public string? Model { get; }
    public int Type { get; }
    public int? AmperageCapacity { get; }
    public DateTime InstallationDate { get; }
    public DateTime? LastCalibrationDate { get; }
    public int Status { get; }

    public CreateMeterCommand(
        Guid meterId,
        string meterNumber,
        Guid supplyId,
        string? brand,
        string? model,
        int type,
        int? amperageCapacity,
        DateTime installationDate,
        DateTime? lastCalibrationDate,
        int status) : base(meterId)
    {
        MeterNumber = meterNumber;
        SupplyId = supplyId;
        Brand = brand;
        Model = model;
        Type = type;
        AmperageCapacity = amperageCapacity;
        InstallationDate = installationDate;
        LastCalibrationDate = lastCalibrationDate;
        Status = status;
    }

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}