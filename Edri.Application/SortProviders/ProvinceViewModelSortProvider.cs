using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class ProvinceViewModelSortProvider : ISortingExpressionProvider<ProvinceViewModel, Province>
{
    private static readonly Dictionary<string, Expression<Func<Province, object>>> s_expressions = new()
    {
        { "id", Province => Province.Id },
        {"departmentId",Department => Department.DepartmentId  },
        { "name", Province => Province.Name },
        {  "code", Province => Province.Code   }
    };

    public Dictionary<string, Expression<Func<Province, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}