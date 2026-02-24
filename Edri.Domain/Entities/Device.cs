using System;

namespace Edri.Domain.Entities;

public class Device : Entity
{
    public string Code { get; private set; }
    public Guid MeterId { get; private set; }
    public Guid BranchId { get; private set; }
    public string? MacAddress { get; private set; }
    public string? Model { get; private set; }
    public string? FirmwareVersion { get; private set; }
    public int Status { get; private set; }
    public DateTime? LastConnection { get; private set; }
    public DateTime InstallationDate { get; private set; }

    public virtual Meter Meter { get; private set; } = null!;
    public virtual Branch Branch { get; private set; } = null!;

    public Device(
        Guid id,
        string code,
        Guid meterId,
        Guid branchId,
        string? macAddress,
        string? model,
        string? firmwareVersion,
        int status,
        DateTime? lastConnection,
        DateTime installationDate) : base(id)
    {
        Code = code;
        MeterId = meterId;
        BranchId = branchId;
        MacAddress = macAddress;
        Model = model;
        FirmwareVersion = firmwareVersion;
        Status = status;
        LastConnection = lastConnection;
        InstallationDate = installationDate;
    }

    public void Update(
        string code,
        Guid meterId,
        Guid branchId,
        string? macAddress,
        string? model,
        string? firmwareVersion,
        int status,
        DateTime? lastConnection,
        DateTime installationDate)
    {
        Code = code;
        MeterId = meterId;
        BranchId = branchId;
        MacAddress = macAddress;
        Model = model;
        FirmwareVersion = firmwareVersion;
        Status = status;
        LastConnection = lastConnection;
        InstallationDate = installationDate;
    }

    public void UpdateLastConnection(DateTime lastConnection)
    {
        LastConnection = lastConnection;
    }
}