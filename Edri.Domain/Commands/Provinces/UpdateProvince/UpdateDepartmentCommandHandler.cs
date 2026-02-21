using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Province;
using MediatR;

namespace Edri.Domain.Commands.Provinces.UpdateProvince;

public sealed class UpdateProvinceCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateProvinceCommand>
{
    private readonly IProvinceRepository _ProvinceRepository;
    private readonly IUser _user;

    public UpdateProvinceCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IProvinceRepository ProvinceRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _ProvinceRepository = ProvinceRepository;
        _user = user;
    }

    public async Task Handle(UpdateProvinceCommand request, CancellationToken cancellationToken)
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
                $"No permission to update Province {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); // String directo
            return;
        }

        // 3. Obtener la entidad
        var Province = await _ProvinceRepository.GetByIdAsync(request.AggregateId);

        if (Province is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Province with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND")); // String directo
            return;
        }

        // 5. Actualizar Entidad
        // Usamos el método Update de la entidad para cambiar Nombre y Código a la vez
        Province.Update(Province.DepartmentId,Province.Name,Province.Code);

        _ProvinceRepository.Update(Province);

        // 6. Persistir y lanzar evento
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new ProvinceUpdatedEvent(
                Province.Id,
                Province.DepartmentId,
                Province.Name,
                Province.Code));
        }
    }
}