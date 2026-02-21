using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Province; // Asegúrate de actualizar tu Evento también
using MediatR;

namespace Edri.Domain.Commands.Provinces.CreateProvince;

public sealed class CreateProvinceCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateProvinceCommand>
{
    private readonly IProvinceRepository _ProvinceRepository;
    private readonly IUser _user;

    public CreateProvinceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProvinceRepository ProvinceRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProvinceRepository = ProvinceRepository;
        _user = user;
    }

    public async Task Handle(CreateProvinceCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Province {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); 

            return;
        }
        if (await _ProvinceRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Province with Id {request.AggregateId}",
                "Province_ALREADY_EXISTS")); 

            return;
        }
        var Province = new Province(
            request.AggregateId,
            request.DepartmentId,
            request.Name,
            request.Code); 

        _ProvinceRepository.Add(Province);
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProvinceCreatedEvent(
                Province.Id,
                Province.DepartmentId,
                Province.Name,
                Province.Code));
        }
    }
}