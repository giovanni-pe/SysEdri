using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;

namespace Edri.Application.Interfaces;

public interface IDepartmentService
{
    public Task<Guid> CreateDepartmentAsync(CreateDepartmentViewModel Department);
    public Task UpdateDepartmentAsync(UpdateDepartmentViewModel Department);
    public Task DeleteDepartmentAsync(Guid DepartmentId);
    public Task<DepartmentViewModel?> GetDepartmentByIdAsync(Guid DepartmentId);

    public Task<PagedResult<DepartmentViewModel>> GetAllDepartmentsAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}