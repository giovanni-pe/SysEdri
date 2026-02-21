using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;
using Edri.Domain.Entities;

namespace Edri.Application.SortProviders;

public sealed class CustomerViewModelSortProvider : ISortingExpressionProvider<CustomerViewModel, Customer>
{
    private static readonly Dictionary<string, Expression<Func<Customer, object>>> s_expressions = new()
    {
        { "id", customer => customer.Id },
        { "userId", customer => customer.UserId },
        { "districtId", customer => customer.DistrictId },
        { "documentType", customer => customer.DocumentType },
        { "documentNumber", customer => customer.DocumentNumber },
        { "customerType", customer => customer.CustomerType },
        { "status", customer => customer.Status },
        { "registrationDate", customer => customer.RegistrationDate }
    };

    public Dictionary<string, Expression<Func<Customer, object>>> GetSortingExpressions()
    {
        return s_expressions;
    }
}