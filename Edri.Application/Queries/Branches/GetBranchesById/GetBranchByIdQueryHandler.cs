using System.Threading;
using System.Threading.Tasks;
using Edri.Application.ViewModels.Branches;
using Edri.Domain.Errors;
using Edri.Domain.Interfaces;
using Edri.Domain.Interfaces.Repositories;
using Edri.Domain.Notifications;
using MediatR;

namespace Edri.Application.Queries.Branches.GetBranchById;

public sealed class GetBranchByIdQueryHandler :
    IRequestHandler<GetBranchByIdQuery, BranchViewModel?>
{
    private readonly IMediatorHandler _bus;
    private readonly IBranchRepository _branchRepository;

    public GetBranchByIdQueryHandler(IBranchRepository branchRepository, IMediatorHandler bus)
    {
        _branchRepository = branchRepository;
        _bus = bus;
    }

    public async Task<BranchViewModel?> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.BranchId);

        if (branch is null)
        {
            await _bus.RaiseEventAsync(
                new DomainNotification(
                    nameof(GetBranchByIdQuery),
                    $"Branch with id {request.BranchId} could not be found",
                    ErrorCodes.ObjectNotFound));
            return null;
        }

        return BranchViewModel.FromBranch(branch);
    }
}