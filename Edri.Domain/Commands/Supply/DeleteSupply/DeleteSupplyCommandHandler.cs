using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Supply;
using MediatR;

namespace Edri.Domain.Commands.Supplies.DeleteSupply;

public sealed class DeleteSupplyCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteSupplyCommand>
{
    private readonly ISupplyRepository _supplyRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteSupplyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISupplyRepository supplyRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _supplyRepository = supplyRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteSupplyCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Supply {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var supply = await _supplyRepository.GetByIdAsync(request.AggregateId);

        if (supply is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Supply with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _supplyRepository.Remove(supply);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SupplyDeletedEvent(supply.Id));
        }
    }
}