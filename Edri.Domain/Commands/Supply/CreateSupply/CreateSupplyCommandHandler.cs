using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Supply;
using MediatR;

namespace Edri.Domain.Commands.Supplies.CreateSupply;

public sealed class CreateSupplyCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateSupplyCommand>
{
    private readonly ISupplyRepository _supplyRepository;
    private readonly IUser _user;

    public CreateSupplyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISupplyRepository supplyRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _supplyRepository = supplyRepository;
        _user = user;
    }

    public async Task Handle(CreateSupplyCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Supply {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _supplyRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Supply with Id {request.AggregateId}",
                "SUPPLY_ALREADY_EXISTS"));
            return;
        }

        var supply = new Supply(
            request.AggregateId,
            request.SupplyNumber,
            request.CustomerId,
            request.TariffId,
            request.BranchId,
            request.DistrictId,
            request.InstallationAddress,
            request.Reference,
            request.Latitude,
            request.Longitude,
            request.Status,
            request.ActivationDate,
            request.TerminationDate);

        _supplyRepository.Add(supply);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SupplyCreatedEvent(
                supply.Id,
                supply.SupplyNumber,
                supply.CustomerId));
        }
    }
}