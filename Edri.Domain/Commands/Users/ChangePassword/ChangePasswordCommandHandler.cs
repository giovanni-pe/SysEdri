using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.User;
using MediatR;
using BC = BCrypt.Net.BCrypt;

namespace Edri.Domain.Commands.Users.ChangePassword;

public sealed class ChangePasswordCommandHandler : CommandHandlerBase,
    IRequestHandler<ChangePasswordCommand>
{
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        var user = await _userRepository.GetByIdAsync(_user.GetUserId());

        if (user is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no user with Id {_user.GetUserId()}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        if (!BC.Verify(request.Password, user.Password))
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    "The password is incorrect",
                    DomainErrorCodes.User.PasswordIncorrect));

            return;
        }

        var passwordHash = BC.HashPassword(request.NewPassword);
        user.SetPassword(passwordHash);

        _userRepository.Update(user);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new PasswordChangedEvent(user.Id));
        }
    }
}