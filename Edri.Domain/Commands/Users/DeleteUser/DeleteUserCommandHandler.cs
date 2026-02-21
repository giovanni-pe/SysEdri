using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.User;
using MediatR;

namespace Edri.Domain.Commands.Users.DeleteUser;

public sealed class DeleteUserCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteUserCommand>
{
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {request.UserId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (_user.GetUserId() != request.UserId && _user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete user {request.UserId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        _userRepository.Remove(user);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new UserDeletedEvent(request.UserId, user.TenantId));
        }
    }
}