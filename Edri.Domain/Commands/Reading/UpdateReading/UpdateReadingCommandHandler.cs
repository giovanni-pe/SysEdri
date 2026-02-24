using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Reading;
using MediatR;

namespace Edri.Domain.Commands.Readings.UpdateReading;

public sealed class UpdateReadingCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateReadingCommand>
{
    private readonly IReadingRepository _readingRepository;
    private readonly IUser _user;

    public UpdateReadingCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IReadingRepository readingRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _readingRepository = readingRepository;
        _user = user;
    }

    public async Task Handle(UpdateReadingCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Reading {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var reading = await _readingRepository.GetByIdAsync(request.AggregateId);

        if (reading is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Reading with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        reading.Update(
            request.DeviceId,
            request.CaptureId,
            request.MeterNumber,
            request.ValueKwh,
            request.ReadingDate,
            request.Source,
            request.OcrConfidence,
            request.Observations);

        _readingRepository.Update(reading);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ReadingUpdatedEvent(
                reading.Id,
                reading.MeterNumber,
                reading.ValueKwh));
        }
    }
}