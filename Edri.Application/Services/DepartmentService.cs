using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Departments.GetAll;
using Edri.Application.Queries.Departments.GetDepartmentById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;
using Edri.Domain;
using Edri.Domain.Commands.Departments.CreateDepartment;
using Edri.Domain.Commands.Departments.DeleteDepartment;
using Edri.Domain.Commands.Departments.UpdateDepartment;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class DepartmentService : IDepartmentService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public DepartmentService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateDepartmentAsync(CreateDepartmentViewModel Department)
    {
        var DepartmentId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateDepartmentCommand(
            DepartmentId,
            Department.Name,Department.Code));

        return DepartmentId;
    }

    public async Task UpdateDepartmentAsync(UpdateDepartmentViewModel Department)
    {
        await _bus.SendCommandAsync(new UpdateDepartmentCommand(
            Department.Id,
            Department.Name,Department.Code));
    }

    public async Task DeleteDepartmentAsync(Guid DepartmentId)
    {
        await _bus.SendCommandAsync(new DeleteDepartmentCommand(DepartmentId));
    }

    public async Task<DepartmentViewModel?> GetDepartmentByIdAsync(Guid DepartmentId)
    {
        var cachedDepartment = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Department>(DepartmentId),
            async () => await _bus.QueryAsync(new GetDepartmentByIdQuery(DepartmentId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedDepartment;
    }

    public async Task<PagedResult<DepartmentViewModel>> GetAllDepartmentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllDepartmentsQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}