using System;
using System.Threading.Tasks;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;

namespace Edri.Application.Interfaces;

public interface ICustomerService
{
    Task<Guid> CreateCustomerAsync(CreateCustomerViewModel customer);
    Task UpdateCustomerAsync(UpdateCustomerViewModel customer);
    Task DeleteCustomerAsync(Guid customerId);
    Task<CustomerViewModel?> GetCustomerByIdAsync(Guid customerId);
    Task<PagedResult<CustomerViewModel>> GetAllCustomersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null);
}