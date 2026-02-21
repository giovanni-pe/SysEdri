using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Users;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Users.GetAll;

public sealed class GetAllUsersQueryHandler :
    IRequestHandler<GetAllUsersQuery, PagedResult<UserViewModel>>
{
    private readonly ISortingExpressionProvider<UserViewModel, User> _sortingExpressionProvider;
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(
        IUserRepository userRepository,
        ISortingExpressionProvider<UserViewModel, User> sortingExpressionProvider)
    {
        _userRepository = userRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<UserViewModel>> Handle(
        GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var usersQuery = _userRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            usersQuery = usersQuery.Where(user =>
                user.Email.Contains(request.SearchTerm) ||
                user.FirstName.Contains(request.SearchTerm) ||
                user.LastName.Contains(request.SearchTerm));
        }

        var totalCount = await usersQuery.CountAsync(cancellationToken);

        usersQuery = usersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var users = await usersQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(user => UserViewModel.FromUser(user))
            .ToListAsync(cancellationToken);

        return new PagedResult<UserViewModel>(
            totalCount, users, request.Query.Page, request.Query.PageSize);
    }
}