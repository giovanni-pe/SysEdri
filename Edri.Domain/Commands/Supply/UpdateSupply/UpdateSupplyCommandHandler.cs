using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Supply;
using MediatR;

namespace Edri.Domain.Commands.Supplies.UpdateSupply;

public sealed class UpdateSupplyCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateSupplyCommand>
{
    private readonly ISupplyRepository _supplyRepository;
    private readonly IUser _user;

    public UpdateSupplyCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        ISupplyRepository supplyRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _supplyRepository = supplyRepository;
        _user = user;
    }

    public async Task Handle(UpdateSupplyCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Supply {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var supply = await _supplyRepository.GetByIdAsync(request.AggregateId);

        if (supply is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Supply with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        supply.Update(
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

        _supplyRepository.Update(supply);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new SupplyUpdatedEvent(
                supply.Id,
                supply.SupplyNumber,
                supply.CustomerId));
        }
    }
}