using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;
using MediatR;

namespace Edri.Application.Queries.Customers.GetAll;

public sealed record GetAllCustomersQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<CustomerViewModel>>;