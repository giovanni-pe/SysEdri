using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Supplies;
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
public sealed class SupplyController : ApiController
{
    private readonly ISupplyService _supplyService;

    public SupplyController(
        INotificationHandler<DomainNotification> notifications,
        ISupplyService supplyService) : base(notifications)
    {
        _supplyService = supplyService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Supplies")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<SupplyViewModel>>))]
    public async Task<IActionResult> GetAllSuppliesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<SupplyViewModelSortProvider, SupplyViewModel, Supply>]
        SortQuery? sortQuery = null)
    {
        var supplies = await _supplyService.GetAllSuppliesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(supplies);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Supply by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<SupplyViewModel>))]
    public async Task<IActionResult> GetSupplyByIdAsync([FromRoute] Guid id)
    {
        var supply = await _supplyService.GetSupplyByIdAsync(id);
        return Response(supply);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Supply")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateSupplyAsync([FromBody] CreateSupplyViewModel supply)
    {
        var supplyId = await _supplyService.CreateSupplyAsync(supply);
        return Response(supplyId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Supply")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateSupplyViewModel>))]
    public async Task<IActionResult> UpdateSupplyAsync([FromBody] UpdateSupplyViewModel supply)
    {
        await _supplyService.UpdateSupplyAsync(supply);
        return Response(supply);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Supply")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteSupplyAsync([FromRoute] Guid id)
    {
        await _supplyService.DeleteSupplyAsync(id);
        return Response(id);
    }
}