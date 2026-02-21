using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Meter;
using MediatR;

namespace Edri.Domain.Commands.Meters.DeleteMeter;

public sealed class DeleteMeterCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteMeterCommand>
{
    private readonly IMeterRepository _meterRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteMeterCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IMeterRepository meterRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _meterRepository = meterRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteMeterCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Meter {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var meter = await _meterRepository.GetByIdAsync(request.AggregateId);

        if (meter is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Meter with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _meterRepository.Remove(meter);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new MeterDeletedEvent(meter.Id));
        }
    }
}