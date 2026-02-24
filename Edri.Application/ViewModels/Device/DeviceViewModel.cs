using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Devices;

public sealed class DeviceViewModel
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public Guid MeterId { get; set; }
    public Guid BranchId { get; set; }
    public string? MacAddress { get; set; }
    public string? Model { get; set; }
    public string? FirmwareVersion { get; set; }
    public int Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public DateTime? LastConnection { get; set; }
    public DateTime InstallationDate { get; set; }

    public static DeviceViewModel FromDevice(Device device)
    {
        return new DeviceViewModel
        {
            Id = device.Id,
            Code = device.Code,
            MeterId = device.MeterId,
            BranchId = device.BranchId,
            MacAddress = device.MacAddress,
            Model = device.Model,
            FirmwareVersion = device.FirmwareVersion,
            Status = device.Status,
            StatusDescription = device.Status switch
            {
                1 => "Active",
                2 => "Inactive",
                3 => "Maintenance",
                _ => "Unknown"
            },
            LastConnection = device.LastConnection,
            InstallationDate = device.InstallationDate
        };
    }
}