using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class MeterViewModelSortProvider : ISortingExpressionProvider<MeterViewModel, Meter>
{
    private static readonly Dictionary<string, Expression<Func<Meter, object>>> s_expressions = new()
    {
        { "id", meter => meter.Id },
        { "meterNumber", meter => meter.MeterNumber },
        { "supplyId", meter => meter.SupplyId },
        { "brand", meter => meter.Brand! },
        { "model", meter => meter.Model! },
        { "type", meter => meter.Type },
        { "status", meter => meter.Status },
        { "installationDate", meter => meter.InstallationDate }
    };

    public Dictionary<string, Expression<Func<Meter, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}