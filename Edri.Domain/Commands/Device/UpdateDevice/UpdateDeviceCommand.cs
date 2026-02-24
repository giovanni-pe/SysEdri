using System;

namespace Edri.Domain.Commands.Devices.UpdateDevice;

public sealed class UpdateDeviceCommand : CommandBase
{
    private static readonly UpdateDeviceCommandValidation s_validation = new();

    public string Code { get; }
    public Guid MeterId { get; }
    public Guid BranchId { get; }
    public string? MacAddress { get; }
    public string? Model { get; }
    public string? FirmwareVersion { get; }
    public int Status { get; }
    public DateTime? LastConnection { get; }
    public DateTime InstallationDate { get; }

    public UpdateDeviceCommand(
        Guid deviceId,
        string code,
        Guid meterId,
        Guid branchId,
        string? macAddress,
        string? model,
        string? firmwareVersion,
        int status,
        DateTime? lastConnection,
        DateTime installationDate) : base(deviceId)
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

    public override bool IsValid()
    {
        ValidationResult = s_validation.Validate(this);
        return ValidationResult.IsValid;
    }
}