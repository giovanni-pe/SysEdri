using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Branch;
using MediatR;

namespace Edri.Domain.Commands.Branches.CreateBranch;

public sealed class CreateBranchCommandHandler : CommandHandlerBase,
    IRequestHandler<CreateBranchCommand>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUser _user;

    public CreateBranchCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IBranchRepository branchRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _branchRepository = branchRepository;
        _user = user;
    }

    public async Task Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to create Branch {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        if (await _branchRepository.ExistsAsync(request.AggregateId))
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is already a Branch with Id {request.AggregateId}",
                "BRANCH_ALREADY_EXISTS"));
            return;
        }

        var branch = new Branch(
            request.AggregateId,
            request.CompanyId,
            request.DistrictId,
            request.Name,
            request.Address,
            request.Phone,
            request.IsActive);

        _branchRepository.Add(branch);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new BranchCreatedEvent(
                branch.Id,
                branch.CompanyId,
                branch.Name));
        }
    }
}