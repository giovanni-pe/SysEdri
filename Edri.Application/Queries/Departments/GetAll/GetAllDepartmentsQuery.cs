using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;
using MediatR;

namespace Edri.Application.Queries.Departments.GetAll;

public sealed record GetAllDepartmentsQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<DepartmentViewModel>>;