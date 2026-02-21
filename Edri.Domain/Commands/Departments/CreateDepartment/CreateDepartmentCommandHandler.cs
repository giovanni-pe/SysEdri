using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Department; // Asegúrate de actualizar tu Evento también
using MediatR;

namespace Edri.Domain.Commands.Departments.CreateDepartment;

public sealed class CreateDepartmentCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateDepartmentCommand>
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IUser _user;

    public CreateDepartmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDepartmentRepository departmentRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _departmentRepository = departmentRepository;
        _user = user;
    }

    public async Task Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }
        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Department {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS")); 

            return;
        }
        if (await _departmentRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Department with Id {request.AggregateId}",
                "DEPARTMENT_ALREADY_EXISTS")); 

            return;
        }
        var department = new Department(
            request.AggregateId,
            request.Name,
            request.Code); 

        _departmentRepository.Add(department);
        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DepartmentCreatedEvent(
                department.Id,
                department.Name,
                department.Code));
        }
    }
}