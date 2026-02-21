using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class DistrictViewModelSortProvider : ISortingExpressionProvider<DistrictViewModel, District>
{
    private static readonly Dictionary<string, Expression<Func<District, object>>> s_expressions = new()
    {
        { "id", District => District.Id },
        {"provinceId",Province => Province.ProvinceId  },
        { "name", District => District.Name },
        {  "code", District => District.Code   }
    };

    public Dictionary<string, Expression<Func<District, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}