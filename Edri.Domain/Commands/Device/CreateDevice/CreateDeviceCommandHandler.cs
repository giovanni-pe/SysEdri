using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Device;
using MediatR;

namespace Edri.Domain.Commands.Devices.CreateDevice;

public sealed class CreateDeviceCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateDeviceCommand>
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUser _user;

    public CreateDeviceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDeviceRepository deviceRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _deviceRepository = deviceRepository;
        _user = user;
    }

    public async Task Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Device {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _deviceRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Device with Id {request.AggregateId}",
                "DEVICE_ALREADY_EXISTS"));
            return;
        }

        var device = new Device(
            request.AggregateId,
            request.Code,
            request.MeterId,
            request.BranchId,
            request.MacAddress,
            request.Model,
            request.FirmwareVersion,
            request.Status,
            request.LastConnection,
            request.InstallationDate);

        _deviceRepository.Add(device);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DeviceCreatedEvent(
                device.Id,
                device.Code,
                device.MeterId));
        }
    }
}