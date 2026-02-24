using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Captures;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Captures.GetCaptureById;

public sealed class GetCaptureByIdQueryHandler :
    IRequestHandler<GetCaptureByIdQuery, CaptureViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly ICaptureRepository _captureRepository;

    public GetCaptureByIdQueryHandler(ICaptureRepository captureRepository, IMediatorHandler bus)
    {
        _captureRepository = captureRepository;
        _bus = bus;
    }

    public async Task<CaptureViewModel?> Handle(GetCaptureByIdQuery request, CancellationToken cancellationToken)
    {
        var capture = await _captureRepository.GetByIdAsync(request.CaptureId);

        if (capture is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetCaptureByIdQuery),
                    $"Capture with id {request.CaptureId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return CaptureViewModel.FromCapture(capture);
    }
}