using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Meter;
using MediatR;

namespace Edri.Domain.Commands.Meters.CreateMeter;

public sealed class CreateMeterCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateMeterCommand>
{
    private readonly IMeterRepository _meterRepository;
    private readonly IUser _user;

    public CreateMeterCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IMeterRepository meterRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _meterRepository = meterRepository;
        _user = user;
    }

    public async Task Handle(CreateMeterCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Meter {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _meterRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Meter with Id {request.AggregateId}",
                "METER_ALREADY_EXISTS"));
            return;
        }

        var meter = new Meter(
            request.AggregateId,
            request.MeterNumber,
            request.SupplyId,
            request.Brand,
            request.Model,
            request.Type,
            request.AmperageCapacity,
            request.InstallationDate,
            request.LastCalibrationDate,
            request.Status);

        _meterRepository.Add(meter);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new MeterCreatedEvent(
                meter.Id,
                meter.MeterNumber,
                meter.SupplyId));
        }
    }
}