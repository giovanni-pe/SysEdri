using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tenants;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class TenantViewModelSortProvider : ISortingExpressionProvider<TenantViewModel, Tenant>
{
    private static readonly Dictionary<string, Expression<Func<Tenant, object>>> s_expressions = new()
    {
        { "id", tenant => tenant.Id },
        { "name", tenant => tenant.Name }
    };

    public Dictionary<string, Expression<Func<Tenant, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}