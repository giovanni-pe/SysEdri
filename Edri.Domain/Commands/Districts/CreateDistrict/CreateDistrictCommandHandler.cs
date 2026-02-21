using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.District; // Asegúrate de actualizar tu Evento también
using MediatR;

namespace Edri.Domain.Commands.Districts.CreateDistrict;

public sealed class CreateDistrictCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateDistrictCommand>
{
    private readonly IDistrictRepository _DistrictRepository;
    private readonly IUser _user;

    public CreateDistrictCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDistrictRepository DistrictRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _DistrictRepository = DistrictRepository;
        _user = user;
    }

    public async Task Handle(CreateDistrictCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create District {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); 

            return;
        }
        if (await _DistrictRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a District with Id {request.AggregateId}",
                "District_ALREADY_EXISTS")); 

            return;
        }
        var District = new District(
            request.AggregateId,
            request.ProvinceId,
            request.Name,
            request.Code); 

        _DistrictRepository.Add(District);
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DistrictCreatedEvent(
                District.Id,
                District.ProvinceId,
                District.Name,
                District.Code));
        }
    }
}