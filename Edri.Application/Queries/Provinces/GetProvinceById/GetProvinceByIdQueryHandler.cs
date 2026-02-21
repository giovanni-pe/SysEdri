using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Provinces;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Provinces.GetProvinceById;

public sealed class GetProvinceByIdQueryHandler :
    IRequestHandler<GetProvinceByIdQuery, ProvinceViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IProvinceRepository _ProvinceRepository;

    public GetProvinceByIdQueryHandler(IProvinceRepository ProvinceRepository, IMediatorHandler bus)
    {
        _ProvinceRepository = ProvinceRepository;
        _bus = bus;
    }

    public async Task<ProvinceViewModel?> Handle(GetProvinceByIdQuery request, CancellationToken cancellationToken)
    {
        var Province = await _ProvinceRepository.GetByIdAsync(request.ProvinceId);

        if (Province is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetProvinceByIdQuery),
                    $"Province with id {request.ProvinceId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ProvinceViewModel.FromProvince(Province);
    }
}