using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Capture;
using MediatR;

namespace Edri.Domain.Commands.Captures.DeleteCapture;

public sealed class DeleteCaptureCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteCaptureCommand>
{
    private readonly ICaptureRepository _captureRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteCaptureCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ICaptureRepository captureRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _captureRepository = captureRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteCaptureCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Capture {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var capture = await _captureRepository.GetByIdAsync(request.AggregateId);

        if (capture is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Capture with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _captureRepository.Remove(capture);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new CaptureDeletedEvent(capture.Id));
        }
    }
}