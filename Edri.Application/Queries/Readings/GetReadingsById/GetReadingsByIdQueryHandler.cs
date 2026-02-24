using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Readings;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Readings.GetReadingById;

public sealed class GetReadingByIdQueryHandler :
    IRequestHandler<GetReadingByIdQuery, ReadingViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IReadingRepository _readingRepository;

    public GetReadingByIdQueryHandler(IReadingRepository readingRepository, IMediatorHandler bus)
    {
        _readingRepository = readingRepository;
        _bus = bus;
    }

    public async Task<ReadingViewModel?> Handle(GetReadingByIdQuery request, CancellationToken cancellationToken)
    {
        var reading = await _readingRepository.GetByIdAsync(request.ReadingId);

        if (reading is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetReadingByIdQuery),
                    $"Reading with id {request.ReadingId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return ReadingViewModel.FromReading(reading);
    }
}