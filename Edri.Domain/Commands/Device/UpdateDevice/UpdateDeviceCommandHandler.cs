using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Device;
using MediatR;

namespace Edri.Domain.Commands.Devices.UpdateDevice;

public sealed class UpdateDeviceCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateDeviceCommand>
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUser _user;

    public UpdateDeviceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDeviceRepository deviceRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _deviceRepository = deviceRepository;
        _user = user;
    }

    public async Task Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Device {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var device = await _deviceRepository.GetByIdAsync(request.AggregateId);

        if (device is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Device with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        device.Update(
            request.Code,
            request.MeterId,
            request.BranchId,
            request.MacAddress,
            request.Model,
            request.FirmwareVersion,
            request.Status,
            request.LastConnection,
            request.InstallationDate);

        _deviceRepository.Update(device);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DeviceUpdatedEvent(
                device.Id,
                device.Code,
                device.MeterId));
        }
    }
}