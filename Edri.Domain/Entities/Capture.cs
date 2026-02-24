using System;

namespace Edri.Domain.Entities;

public class Capture : Entity
{
    public Guid DeviceId { get; private set; }
    public DateTime Timestamp { get; private set; }
    public string ImageUrl { get; private set; }
    public int Status { get; private set; }

    public virtual Device Device { get; private set; } = null!;

    public Capture(
        Guid id,
        Guid deviceId,
        DateTime timestamp,
        string imageUrl,
        int status) : base(id)
    {
        DeviceId = deviceId;
        Timestamp = timestamp;
        ImageUrl = imageUrl;
        Status = status;
    }

    public void Update(
        Guid deviceId,
        DateTime timestamp,
        string imageUrl,
        int status)
    {
        DeviceId = deviceId;
        Timestamp = timestamp;
        ImageUrl = imageUrl;
        Status = status;
    }

    public void UpdateStatus(int status)
    {
        Status = status;
    }

    public void MarkAsProcessed()
    {
        Status = 2; // Processed
    }

    public void MarkAsFailed()
    {
        Status = 3; // Failed
    }
}