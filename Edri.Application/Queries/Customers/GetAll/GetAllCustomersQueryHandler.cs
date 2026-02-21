using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Customers.GetAll;

public sealed class GetAllCustomersQueryHandler :
    IRequestHandler<GetAllCustomersQuery, PagedResult<CustomerViewModel>>
{
    private readonly ISortingExpressionProvider<CustomerViewModel, Customer> _sortingExpressionProvider;
    private readonly ICustomerRepository _customerRepository;

    public GetAllCustomersQueryHandler(
        ICustomerRepository customerRepository,
        ISortingExpressionProvider<CustomerViewModel, Customer> sortingExpressionProvider)
    {
        _customerRepository = customerRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CustomerViewModel>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customersQuery = _customerRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            customersQuery = customersQuery.Where(customer =>
                customer.DocumentNumber.Contains(request.SearchTerm) ||
                (customer.FirstName != null && customer.FirstName.Contains(request.SearchTerm)) ||
                (customer.LastName != null && customer.LastName.Contains(request.SearchTerm)) ||
                (customer.BusinessName != null && customer.BusinessName.Contains(request.SearchTerm)));
        }

        var totalCount = await customersQuery.CountAsync(cancellationToken);

        customersQuery = customersQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var customers = await customersQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(customer => CustomerViewModel.FromCustomer(customer))
            .ToListAsync(cancellationToken);

        return new PagedResult<CustomerViewModel>(
            totalCount, customers, request.Query.Page, request.Query.PageSize);
    }
}