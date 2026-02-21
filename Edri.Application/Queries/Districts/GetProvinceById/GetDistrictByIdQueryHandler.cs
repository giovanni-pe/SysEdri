using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Districts;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Districts.GetDistrictById;

public sealed class GetDistrictByIdQueryHandler :
    IRequestHandler<GetDistrictByIdQuery, DistrictViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDistrictRepository _DistrictRepository;

    public GetDistrictByIdQueryHandler(IDistrictRepository DistrictRepository, IMediatorHandler bus)
    {
        _DistrictRepository = DistrictRepository;
        _bus = bus;
    }

    public async Task<DistrictViewModel?> Handle(GetDistrictByIdQuery request, CancellationToken cancellationToken)
    {
        var District = await _DistrictRepository.GetByIdAsync(request.DistrictId);

        if (District is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDistrictByIdQuery),
                    $"District with id {request.DistrictId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DistrictViewModel.FromDistrict(District);
    }
}