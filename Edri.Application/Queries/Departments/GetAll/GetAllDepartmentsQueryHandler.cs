using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Departments.GetAll;

public sealed class GetAllDepartmentsQueryHandler :
    IRequestHandler<GetAllDepartmentsQuery, PagedResult<DepartmentViewModel>>
{
    private readonly ISortingExpressionProvider<DepartmentViewModel, Department> _sortingExpressionProvider;
    private readonly IDepartmentRepository _DepartmentRepository;

    public GetAllDepartmentsQueryHandler(
        IDepartmentRepository DepartmentRepository,
        ISortingExpressionProvider<DepartmentViewModel, Department> sortingExpressionProvider)
    {
        _DepartmentRepository = DepartmentRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<DepartmentViewModel>> Handle(
        GetAllDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var DepartmentsQuery = _DepartmentRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null );

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            DepartmentsQuery = DepartmentsQuery.Where(Department =>
                Department.Name.Contains(request.SearchTerm));
        }

        var totalCount = await DepartmentsQuery.CountAsync(cancellationToken);

        DepartmentsQuery = DepartmentsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var Departments = await DepartmentsQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(department => DepartmentViewModel.FromDepartment(department))
            .ToListAsync(cancellationToken);

        return new PagedResult<DepartmentViewModel>(
            totalCount, Departments, request.Query.Page, request.Query.PageSize);
    }
}