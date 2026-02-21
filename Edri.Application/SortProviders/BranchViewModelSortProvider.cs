using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class BranchViewModelSortProvider : ISortingExpressionProvider<BranchViewModel, Branch>
{
    private static readonly Dictionary<string, Expression<Func<Branch, object>>> s_expressions = new()
    {
        { "id", branch => branch.Id },
        { "companyId", branch => branch.CompanyId },
        { "districtId", branch => branch.DistrictId },
        { "name", branch => branch.Name },
        { "isActive", branch => branch.IsActive }
    };

    public Dictionary<string, Expression<Func<Branch, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}