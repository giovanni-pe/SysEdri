using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Province;
using MediatR;

namespace Edri.Domain.Commands.Provinces.DeleteProvince;

public sealed class DeleteProvinceCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteProvinceCommand>
{
    private readonly IProvinceRepository _ProvinceRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteProvinceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProvinceRepository ProvinceRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProvinceRepository = ProvinceRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteProvinceCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Province {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var Province = await _ProvinceRepository.GetByIdAsync(request.AggregateId);

        if (Province is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Province with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _ProvinceRepository.Remove(Province);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProvinceDeletedEvent(Province.Id));
        }
    }
}