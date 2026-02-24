using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Devices;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Devices.GetDeviceById;

public sealed class GetDeviceByIdQueryHandler :
    IRequestHandler<GetDeviceByIdQuery, DeviceViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDeviceRepository _deviceRepository;

    public GetDeviceByIdQueryHandler(IDeviceRepository deviceRepository, IMediatorHandler bus)
    {
        _deviceRepository = deviceRepository;
        _bus = bus;
    }

    public async Task<DeviceViewModel?> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _deviceRepository.GetByIdAsync(request.DeviceId);

        if (device is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDeviceByIdQuery),
                    $"Device with id {request.DeviceId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DeviceViewModel.FromDevice(device);
    }
}