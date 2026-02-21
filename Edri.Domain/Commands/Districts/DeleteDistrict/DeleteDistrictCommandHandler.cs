using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.District;
using MediatR;

namespace Edri.Domain.Commands.Districts.DeleteDistrict;

public sealed class DeleteDistrictCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteDistrictCommand>
{
    private readonly IDistrictRepository _DistrictRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteDistrictCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDistrictRepository DistrictRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _DistrictRepository = DistrictRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteDistrictCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"No permission to delete District {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var District = await _DistrictRepository.GetByIdAsync(request.AggregateId);

        if (District is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no District with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _DistrictRepository.Remove(District);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DistrictDeletedEvent(District.Id));
        }
    }
}