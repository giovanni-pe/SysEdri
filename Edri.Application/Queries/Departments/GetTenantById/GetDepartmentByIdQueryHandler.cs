using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Departments;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Departments.GetDepartmentById;

public sealed class GetDepartmentByIdQueryHandler :
    IRequestHandler<GetDepartmentByIdQuery, DepartmentViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IDepartmentRepository _DepartmentRepository;

    public GetDepartmentByIdQueryHandler(IDepartmentRepository DepartmentRepository, IMediatorHandler bus)
    {
        _DepartmentRepository = DepartmentRepository;
        _bus = bus;
    }

    public async Task<DepartmentViewModel?> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
    {
        var Department = await _DepartmentRepository.GetByIdAsync(request.DepartmentId);

        if (Department is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetDepartmentByIdQuery),
                    $"Department with id {request.DepartmentId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return DepartmentViewModel.FromDepartment(Department);
    }
}