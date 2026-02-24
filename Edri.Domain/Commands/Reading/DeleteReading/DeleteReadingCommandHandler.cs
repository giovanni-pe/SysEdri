using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Reading;
using MediatR;

namespace Edri.Domain.Commands.Readings.DeleteReading;

public sealed class DeleteReadingCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteReadingCommand>
{
    private readonly IReadingRepository _readingRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteReadingCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IReadingRepository readingRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _readingRepository = readingRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteReadingCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Reading {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var reading = await _readingRepository.GetByIdAsync(request.AggregateId);

        if (reading is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Reading with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _readingRepository.Remove(reading);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ReadingDeletedEvent(reading.Id));
        }
    }
}