using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Branches.GetAll;
using Edri.Application.Queries.Branches.GetBranchById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;
using Edri.Domain;
using Edri.Domain.Commands.Branches.CreateBranch;
using Edri.Domain.Commands.Branches.DeleteBranch;
using Edri.Domain.Commands.Branches.UpdateBranch;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class BranchService : IBranchService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public BranchService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateBranchAsync(CreateBranchViewModel branch)
    {
        var branchId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateBranchCommand(
            branchId,
            branch.CompanyId,
            branch.DistrictId,
            branch.Name,
            branch.Address,
            branch.Phone,
            branch.IsActive));

        return branchId;
    }

    public async Task UpdateBranchAsync(UpdateBranchViewModel branch)
    {
        await _bus.SendCommandAsync(new UpdateBranchCommand(
            branch.Id,
            branch.CompanyId,
            branch.DistrictId,
            branch.Name,
            branch.Address,
            branch.Phone,
            branch.IsActive));
    }

    public async Task DeleteBranchAsync(Guid branchId)
    {
        await _bus.SendCommandAsync(new DeleteBranchCommand(branchId));
    }

    public async Task<BranchViewModel?> GetBranchByIdAsync(Guid branchId)
    {
        var cachedBranch = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Branch>(branchId),
            async () => await _bus.QueryAsync(new GetBranchByIdQuery(branchId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedBranch;
    }

    public async Task<PagedResult<BranchViewModel>> GetAllBranchesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllBranchesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}