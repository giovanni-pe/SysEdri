using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Users;
using MediatR;

namespace Edri.Application.Queries.Users.GetAll;

public sealed record GetAllUsersQuery(
    PageQuery Query,
    bool IncludeDeleted,
    string SearchTerm = "",
    SortQuery? SortQuery = null) :
    IRequest<PagedResult<UserViewModel>>;