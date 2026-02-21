using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Customers.GetAll;
using Edri.Application.Queries.Customers.GetCustomerById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;
using Edri.Domain;
using Edri.Domain.Commands.Customers.CreateCustomer;
using Edri.Domain.Commands.Customers.DeleteCustomer;
using Edri.Domain.Commands.Customers.UpdateCustomer;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class CustomerService : ICustomerService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public CustomerService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateCustomerAsync(CreateCustomerViewModel customer)
    {
        var customerId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateCustomerCommand(
            customerId,
            customer.UserId,
            customer.DistrictId,
            customer.DocumentType,
            customer.DocumentNumber,
            customer.CustomerType,
            customer.BusinessName,
            customer.FirstName,
            customer.LastName,
            customer.Address,
            customer.Phone,
            customer.Email,
            customer.Status));

        return customerId;
    }

    public async Task UpdateCustomerAsync(UpdateCustomerViewModel customer)
    {
        await _bus.SendCommandAsync(new UpdateCustomerCommand(
            customer.Id,
            customer.UserId,
            customer.DistrictId,
            customer.DocumentType,
            customer.DocumentNumber,
            customer.CustomerType,
            customer.BusinessName,
            customer.FirstName,
            customer.LastName,
            customer.Address,
            customer.Phone,
            customer.Email,
            customer.Status));
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        await _bus.SendCommandAsync(new DeleteCustomerCommand(customerId));
    }

    public async Task<CustomerViewModel?> GetCustomerByIdAsync(Guid customerId)
    {
        var cachedCustomer = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Customer>(customerId),
            async () => await _bus.QueryAsync(new GetCustomerByIdQuery(customerId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedCustomer;
    }

    public async Task<PagedResult<CustomerViewModel>> GetAllCustomersAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllCustomersQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}