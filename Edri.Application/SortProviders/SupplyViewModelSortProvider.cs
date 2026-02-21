using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class SupplyViewModelSortProvider : ISortingExpressionProvider<SupplyViewModel, Supply>
{
    private static readonly Dictionary<string, Expression<Func<Supply, object>>> s_expressions = new()
    {
        { "id", supply => supply.Id },
        { "supplyNumber", supply => supply.SupplyNumber },
        { "customerId", supply => supply.CustomerId },
        { "tariffId", supply => supply.TariffId },
        { "branchId", supply => supply.BranchId },
        { "districtId", supply => supply.DistrictId },
        { "status", supply => supply.Status },
        { "activationDate", supply => supply.ActivationDate }
    };

    public Dictionary<string, Expression<Func<Supply, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}