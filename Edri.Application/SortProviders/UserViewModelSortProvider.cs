using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Users;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class UserViewModelSortProvider : ISortingExpressionProvider<UserViewModel, User>
{
    private static readonly Dictionary<string, Expression<Func<User, object>>> s_expressions = new()
    {
        { "email", user => user.Email },
        { "firstName", user => user.FirstName },
        { "lastName", user => user.LastName },
        { "tenantId", user => user.TenantId },
        { "lastloggedindate", user => user.LastLoggedinDate ?? DateTimeOffset.MinValue },
        { "role", user => user.Role },
        { "status", user => user.Status }
    };

    public Dictionary<string, Expression<Func<User, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}