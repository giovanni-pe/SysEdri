using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Capture;
using MediatR;

namespace Edri.Domain.Commands.Captures.UpdateCapture;

public sealed class UpdateCaptureCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateCaptureCommand>
{
    private readonly ICaptureRepository _captureRepository;
    private readonly IUser _user;

    public UpdateCaptureCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICaptureRepository captureRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _captureRepository = captureRepository;
        _user = user;
    }

    public async Task Handle(UpdateCaptureCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Capture {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var capture = await _captureRepository.GetByIdAsync(request.AggregateId);

        if (capture is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Capture with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        capture.Update(
            request.DeviceId,
            request.Timestamp,
            request.ImageUrl,
            request.Status);

        _captureRepository.Update(capture);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CaptureUpdatedEvent(
                capture.Id,
                capture.DeviceId,
                capture.Status));
        }
    }
}