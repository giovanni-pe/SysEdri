using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Department;
using MediatR;

namespace Edri.Domain.Commands.Departments.UpdateDepartment;

public sealed class UpdateDepartmentCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUser _user;

    public UpdateDepartmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDepartmentRepository departmentRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _departmentRepository = departmentRepository;
        _user = user;
    }

    public async Task Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
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
                $"No permission to update Department {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); // String directo
            return;
        }

        // 3. Obtener la entidad
        var department = await _departmentRepository.GetByIdAsync(request.AggregateId);

        if (department is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Department with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND")); // String directo
            return;
        }

        // 5. Actualizar Entidad
        // Usamos el método Update de la entidad para cambiar Nombre y Código a la vez
        department.Update(request.Name, request.Code);

        _departmentRepository.Update(department);

        // 6. Persistir y lanzar evento
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DepartmentUpdatedEvent(
                department.Id,
                department.Name,
                department.Code));
        }
    }
}