using System;

namespace Edri.Domain.Entities;

public class Meter : Entity
{
    public string MeterNumber { get; private set; }
    public Guid SupplyId { get; private set; }
    public string? Brand { get; private set; }
    public string? Model { get; private set; }
    public int Type { get; private set; }
    public int? AmperageCapacity { get; private set; }
    public DateTime InstallationDate { get; private set; }
    public DateTime? LastCalibrationDate { get; private set; }
    public int Status { get; private set; }

    public virtual Supply Supply { get; private set; } = null!;

    public Meter(
        Guid id,
        string meterNumber,
        Guid supplyId,
        string? brand,
        string? model,
        int type,
        int? amperageCapacity,
        DateTime installationDate,
        DateTime? lastCalibrationDate,
        int status) : base(id)
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

    public void Update(
        string meterNumber,
        Guid supplyId,
        string? brand,
        string? model,
        int type,
        int? amperageCapacity,
        DateTime installationDate,
        DateTime? lastCalibrationDate,
        int status)
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
}