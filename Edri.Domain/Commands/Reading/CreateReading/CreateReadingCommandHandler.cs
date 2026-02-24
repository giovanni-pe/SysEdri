using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Reading;
using MediatR;

namespace Edri.Domain.Commands.Readings.CreateReading;

public sealed class CreateReadingCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateReadingCommand>
{
    private readonly IReadingRepository _readingRepository;
    private readonly IUser _user;

    public CreateReadingCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IReadingRepository readingRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _readingRepository = readingRepository;
        _user = user;
    }

    public async Task Handle(CreateReadingCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Reading {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _readingRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Reading with Id {request.AggregateId}",
                "READING_ALREADY_EXISTS"));
            return;
        }

        var reading = new Reading(
            request.AggregateId,
            request.DeviceId,
            request.CaptureId,
            request.MeterNumber,
            request.ValueKwh,
            request.ReadingDate,
            request.Source,
            request.OcrConfidence,
            request.Observations);

        _readingRepository.Add(reading);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ReadingCreatedEvent(
                reading.Id,
                reading.MeterNumber,
                reading.ValueKwh,
                reading.Source));
        }
    }
}