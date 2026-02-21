using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class DepartmentViewModelSortProvider : ISortingExpressionProvider<DepartmentViewModel, Department>
{
    private static readonly Dictionary<string, Expression<Func<Department, object>>> s_expressions = new()
    {
        { "id", Department => Department.Id },
        { "name", Department => Department.Name },
        {  "code", Department => Department.Code   }
    };

    public Dictionary<string, Expression<Func<Department, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}