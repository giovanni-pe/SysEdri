using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Meters;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Meters.GetMeterById;

public sealed class GetMeterByIdQueryHandler :
    IRequestHandler<GetMeterByIdQuery, MeterViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IMeterRepository _meterRepository;

    public GetMeterByIdQueryHandler(IMeterRepository meterRepository, IMediatorHandler bus)
    {
        _meterRepository = meterRepository;
        _bus = bus;
    }

    public async Task<MeterViewModel?> Handle(GetMeterByIdQuery request, CancellationToken cancellationToken)
    {
        var meter = await _meterRepository.GetByIdAsync(request.MeterId);

        if (meter is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetMeterByIdQuery),
                    $"Meter with id {request.MeterId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return MeterViewModel.FromMeter(meter);
    }
}