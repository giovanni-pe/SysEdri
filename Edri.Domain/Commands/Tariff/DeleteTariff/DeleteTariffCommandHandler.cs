using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Tariff;
using MediatR;

namespace Edri.Domain.Commands.Tariffs.DeleteTariff;

public sealed class DeleteTariffCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteTariffCommand>
{
    private readonly ITariffRepository _tariffRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteTariffCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ITariffRepository tariffRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _tariffRepository = tariffRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteTariffCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Tariff {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var tariff = await _tariffRepository.GetByIdAsync(request.AggregateId);

        if (tariff is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Tariff with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _tariffRepository.Remove(tariff);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new TariffDeletedEvent(tariff.Id));
        }
    }
}