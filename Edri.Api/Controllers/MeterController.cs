using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Meters;
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
public sealed class MeterController : ApiController
{
    private readonly IMeterService _meterService;

    public MeterController(
        INotificationHandler<DomainNotification> notifications,
        IMeterService meterService) : base(notifications)
    {
        _meterService = meterService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Meters")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<MeterViewModel>>))]
    public async Task<IActionResult> GetAllMetersAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<MeterViewModelSortProvider, MeterViewModel, Meter>]
        SortQuery? sortQuery = null)
    {
        var meters = await _meterService.GetAllMetersAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(meters);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Meter by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<MeterViewModel>))]
    public async Task<IActionResult> GetMeterByIdAsync([FromRoute] Guid id)
    {
        var meter = await _meterService.GetMeterByIdAsync(id);
        return Response(meter);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Meter")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateMeterAsync([FromBody] CreateMeterViewModel meter)
    {
        var meterId = await _meterService.CreateMeterAsync(meter);
        return Response(meterId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Meter")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateMeterViewModel>))]
    public async Task<IActionResult> UpdateMeterAsync([FromBody] UpdateMeterViewModel meter)
    {
        await _meterService.UpdateMeterAsync(meter);
        return Response(meter);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Meter")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteMeterAsync([FromRoute] Guid id)
    {
        await _meterService.DeleteMeterAsync(id);
        return Response(id);
    }
}