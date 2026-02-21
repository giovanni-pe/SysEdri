using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Branch;
using MediatR;

namespace Edri.Domain.Commands.Branches.UpdateBranch;

public sealed class UpdateBranchCommandHandler : CommandHandlerBase,
    IRequestHandler<UpdateBranchCommand>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUser _user;

    public UpdateBranchCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IBranchRepository branchRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _branchRepository = branchRepository;
        _user = user;
    }

    public async Task Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        if (!await TestValidityAsync(request))
        {
            return;
        }

        if (_user.GetUserRole() != UserRole.Admin)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"No permission to update Branch {request.AggregateId}",
                "INSUFFICIENT_PERMISSIONS"));
            return;
        }

        var branch = await _branchRepository.GetByIdAsync(request.AggregateId);

        if (branch is null)
        {
            await NotifyAsync(new DomainNotification(
                request.MessageType,
                $"There is no Branch with Id {request.AggregateId}",
                "OBJECT_NOT_FOUND"));
            return;
        }

        branch.Update(
            request.CompanyId,
            request.DistrictId,
            request.Name,
            request.Address,
            request.Phone,
            request.IsActive);

        _branchRepository.Update(branch);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new BranchUpdatedEvent(
                branch.Id,
                branch.CompanyId,
                branch.Name));
        }
    }
}