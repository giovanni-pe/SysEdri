using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Device;
using MediatR;

namespace Edri.Domain.Commands.Devices.DeleteDevice;

public sealed class DeleteDeviceCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteDeviceCommand>
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteDeviceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDeviceRepository deviceRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _deviceRepository = deviceRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete Device {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var device = await _deviceRepository.GetByIdAsync(request.AggregateId);

        if (device is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Device with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _deviceRepository.Remove(device);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DeviceDeletedEvent(device.Id));
        }
    }
}