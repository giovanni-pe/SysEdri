using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.District;
using MediatR;

namespace Edri.Domain.Commands.Districts.UpdateDistrict;

public sealed class UpdateDistrictCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateDistrictCommand>
{
    private readonly IDistrictRepository _DistrictRepository;
    private readonly IUser _user;

    public UpdateDistrictCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDistrictRepository DistrictRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _DistrictRepository = DistrictRepository;
        _user = user;
    }

    public async Task Handle(UpdateDistrictCommand request, CancellationToken cancellationToken)
    {
        // 1. Validaciones de formato (FluentValidation)
        if (!await TestValidityAsync(request))
        {
            return;
        }

        // 2. Validación de Permisos (Solo Admin)
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update District {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); // String directo
            return;
        }

        // 3. Obtener la entidad
        var District = await _DistrictRepository.GetByIdAsync(request.AggregateId);

        if (District is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no District with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND")); // String directo
            return;
        }

        // 5. Actualizar Entidad
        // Usamos el método Update de la entidad para cambiar Nombre y Código a la vez
        District.Update(District.ProvinceId,District.Name,District.Code);

        _DistrictRepository.Update(District);

        // 6. Persistir y lanzar evento
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DistrictUpdatedEvent(
                District.Id,
                District.ProvinceId,
                District.Name,
                District.Code));
        }
    }
}