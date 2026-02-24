using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Capture;
using MediatR;

namespace Edri.Domain.Commands.Captures.CreateCapture;

public sealed class CreateCaptureCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateCaptureCommand>
{
    private readonly ICaptureRepository _captureRepository;
    private readonly IUser _user;

    public CreateCaptureCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICaptureRepository captureRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _captureRepository = captureRepository;
        _user = user;
    }

    public async Task Handle(CreateCaptureCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Capture {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _captureRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Capture with Id {request.AggregateId}",
                "CAPTURE_ALREADY_EXISTS"));
            return;
        }

        var capture = new Capture(
            request.AggregateId,
            request.DeviceId,
            request.Timestamp,
            request.ImageUrl,
            request.Status);

        _captureRepository.Add(capture);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CaptureCreatedEvent(
                capture.Id,
                capture.DeviceId,
                capture.ImageUrl));
        }
    }
}