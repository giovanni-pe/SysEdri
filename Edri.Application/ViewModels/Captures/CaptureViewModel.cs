using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Captures;

public sealed class CaptureViewModel
{
    public Guid Id { get; set; }
    public Guid DeviceId { get; set; }
    public DateTime Timestamp { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;

    public static CaptureViewModel FromCapture(Capture capture)
    {
        return new CaptureViewModel
        {
            Id = capture.Id,
            DeviceId = capture.DeviceId,
            Timestamp = capture.Timestamp,
            ImageUrl = capture.ImageUrl,
            Status = capture.Status,
            StatusDescription = capture.Status switch
            {
                1 => "Pending",
                2 => "Processed",
                3 => "Failed",
                _ => "Unknown"
            }
        };
    }
}