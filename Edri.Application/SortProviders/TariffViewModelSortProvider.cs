using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class TariffViewModelSortProvider : ISortingExpressionProvider<TariffViewModel, Tariff>
{
    private static readonly Dictionary<string, Expression<Func<Tariff, object>>> s_expressions = new()
    {
        { "id", tariff => tariff.Id },
        { "companyId", tariff => tariff.CompanyId },
        { "code", tariff => tariff.Code },
        { "name", tariff => tariff.Name },
        { "pricePerKwh", tariff => tariff.PricePerKwh },
        { "fixedCharge", tariff => tariff.FixedCharge },
        { "effectiveFrom", tariff => tariff.EffectiveFrom },
        { "isActive", tariff => tariff.IsActive }
    };

    public Dictionary<string, Expression<Func<Tariff, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}