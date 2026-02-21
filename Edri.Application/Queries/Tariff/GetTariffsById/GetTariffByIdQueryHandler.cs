using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Tariffs;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Tariffs.GetTariffById;

public sealed class GetTariffByIdQueryHandler :
    IRequestHandler<GetTariffByIdQuery, TariffViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ITariffRepository _tariffRepository;

    public GetTariffByIdQueryHandler(ITariffRepository tariffRepository, IMediatorHandler bus)
    {
        _tariffRepository = tariffRepository;
        _bus = bus;
    }

    public async Task<TariffViewModel?> Handle(GetTariffByIdQuery request, CancellationToken cancellationToken)
    {
        var tariff = await _tariffRepository.GetByIdAsync(request.TariffId);

        if (tariff is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetTariffByIdQuery),
                    $"Tariff with id {request.TariffId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return TariffViewModel.FromTariff(tariff);
    }
}