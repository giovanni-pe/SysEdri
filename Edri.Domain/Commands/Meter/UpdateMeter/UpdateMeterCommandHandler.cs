using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Meter;
using MediatR;

namespace Edri.Domain.Commands.Meters.UpdateMeter;

public sealed class UpdateMeterCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateMeterCommand>
{
    private readonly IMeterRepository _meterRepository;
    private readonly IUser _user;

    public UpdateMeterCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IMeterRepository meterRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _meterRepository = meterRepository;
        _user = user;
    }

    public async Task Handle(UpdateMeterCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Meter {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var meter = await _meterRepository.GetByIdAsync(request.AggregateId);

        if (meter is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Meter with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        meter.Update(
            request.MeterNumber,
            request.SupplyId,
            request.Brand,
            request.Model,
            request.Type,
            request.AmperageCapacity,
            request.InstallationDate,
            request.LastCalibrationDate,
            request.Status);

        _meterRepository.Update(meter);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new MeterUpdatedEvent(
                meter.Id,
                meter.MeterNumber,
                meter.SupplyId));
        }
    }
}