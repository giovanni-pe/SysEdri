using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class CompanyViewModelSortProvider : ISortingExpressionProvider<CompanyViewModel, Company>
{
    private static readonly Dictionary<string, Expression<Func<Company, object>>> s_expressions = new()
    {
        { "id", company => company.Id },
        { "taxId", company => company.TaxId },
        { "businessName", company => company.BusinessName },
        { "tradeName", company => company.TradeName! },
        { "email", company => company.Email! }
    };

    public Dictionary<string, Expression<Func<Company, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}