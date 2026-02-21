using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;
using MediatR;

namespace Edri.Application.Queries.Companies.GetAll;

public sealed record GetAllCompaniesQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CompanyViewModel>>;