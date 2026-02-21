using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;

namespace Edri.Application.Interfaces;

public interface ITenantService
{
    public Task<Guid> CreateTenantAsync(CreateTenantViewModel tenant);
    public Task UpdateTenantAsync(UpdateTenantViewModel tenant);
    public Task DeleteTenantAsync(Guid tenantId);
    public Task<TenantViewModel?> GetTenantByIdAsync(Guid tenantId);

    public Task<PagedResult<TenantViewModel>> GetAllTenantsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}