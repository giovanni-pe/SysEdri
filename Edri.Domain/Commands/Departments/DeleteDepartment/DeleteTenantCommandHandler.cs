using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Department;
using MediatR;

namespace Edri.Domain.Commands.Departments.DeleteDepartment;

public sealed class DeleteDepartmentCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteDepartmentCommand>
{
    private readonly IDepartmentRepository _DepartmentRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteDepartmentCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IDepartmentRepository DepartmentRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _DepartmentRepository = DepartmentRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Department {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));

            return;
        }

        var Department = await _DepartmentRepository.GetByIdAsync(request.AggregateId);

        if (Department is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Department with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));

            return;
        }

        _DepartmentRepository.Remove(Department);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new DepartmentDeletedEvent(Department.Id));
        }
    }
}