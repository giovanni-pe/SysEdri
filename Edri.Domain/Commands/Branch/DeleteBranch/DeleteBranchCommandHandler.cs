using System.Threading;
using System.Threading.Tasks;
using Edri.Domain.Enums;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using Edri.Shared.Events.Branch;
using MediatR;

namespace Edri.Domain.Commands.Branches.DeleteBranch;

public sealed class DeleteBranchCommandHandler : CommandHandlerBase,
    IRequestHandler<DeleteBranchCommand>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUser _user;
    private readonly IUserRepository _userRepository;

    public DeleteBranchCommandHandler(
        IMediatorHandler bus,
        IUnitOfWork unitOfWork,
        INotificationHandler<DomainNotification> notifications,
        IBranchRepository branchRepository,
        IUserRepository userRepository,
        IUser user) : base(bus, unitOfWork, notifications)
    {
        _branchRepository = branchRepository;
        _userRepository = userRepository;
        _user = user;
    }

    public async Task Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
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
                    $"No permission to delete Branch {request.AggregateId}",
                    ErrorCodes.InsufficientPermissions));
            return;
        }

        var branch = await _branchRepository.GetByIdAsync(request.AggregateId);

        if (branch is null)
        {
            await NotifyAsync(
                new DomainNotification(
                    request.MessageType,
                    $"There is no Branch with Id {request.AggregateId}",
                    ErrorCodes.ObjectNotFound));
            return;
        }

        _branchRepository.Remove(branch);

        if (await CommitAsync())
        {
            await Bus.RaiseEventAsync(new BranchDeletedEvent(branch.Id));
        }
    }
}