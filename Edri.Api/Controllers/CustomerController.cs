using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Customers;
using Edri.Domain.Entities;
using Edri.Domain.Notifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Edri.Api.Controllers;

[ApiController]
[Authorize]
[Route("/api/v1/[controller]")]
public sealed class CustomerController : ApiController
{
    private readonly ICustomerService _customerService;

    public CustomerController(
        INotificationHandler<DomainNotification> notifications,
        ICustomerService customerService) : base(notifications)
    {
        _customerService = customerService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Customers")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CustomerViewModel>>))]
    public async Task<IActionResult> GetAllCustomersAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<CustomerViewModelSortProvider, CustomerViewModel, Customer>]
        SortQuery? sortQuery = null)
    {
        var customers = await _customerService.GetAllCustomersAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(customers);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Customer by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CustomerViewModel>))]
    public async Task<IActionResult> GetCustomerByIdAsync([FromRoute] Guid id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        return Response(customer);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Customer")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerViewModel customer)
    {
        var customerId = await _customerService.CreateCustomerAsync(customer);
        return Response(customerId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Customer")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCustomerViewModel>))]
    public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerViewModel customer)
    {
        await _customerService.UpdateCustomerAsync(customer);
        return Response(customer);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Customer")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteCustomerAsync([FromRoute] Guid id)
    {
        await _customerService.DeleteCustomerAsync(id);
        return Response(id);
    }
}