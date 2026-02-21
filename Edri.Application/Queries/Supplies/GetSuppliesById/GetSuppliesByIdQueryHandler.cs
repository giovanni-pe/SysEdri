using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Supplies;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Supplies.GetSupplyById;

public sealed class GetSupplyByIdQueryHandler :
    IRequestHandler<GetSupplyByIdQuery, SupplyViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ISupplyRepository _supplyRepository;

    public GetSupplyByIdQueryHandler(ISupplyRepository supplyRepository, IMediatorHandler bus)
    {
        _supplyRepository = supplyRepository;
        _bus = bus;
    }

    public async Task<SupplyViewModel?> Handle(GetSupplyByIdQuery request, CancellationToken cancellationToken)
    {
        var supply = await _supplyRepository.GetByIdAsync(request.SupplyId);

        if (supply is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetSupplyByIdQuery),
                    $"Supply with id {request.SupplyId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return SupplyViewModel.FromSupply(supply);
    }
}