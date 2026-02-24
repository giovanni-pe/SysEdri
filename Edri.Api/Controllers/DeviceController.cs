using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Devices;
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
public sealed class DeviceController : ApiController
{
    private readonly IDeviceService _deviceService;

    public DeviceController(
        INotificationHandler<DomainNotification> notifications,
        IDeviceService deviceService) : base(notifications)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Devices")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DeviceViewModel>>))]
    public async Task<IActionResult> GetAllDevicesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<DeviceViewModelSortProvider, DeviceViewModel, Device>]
        SortQuery? sortQuery = null)
    {
        var devices = await _deviceService.GetAllDevicesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(devices);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Device by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DeviceViewModel>))]
    public async Task<IActionResult> GetDeviceByIdAsync([FromRoute] Guid id)
    {
        var device = await _deviceService.GetDeviceByIdAsync(id);
        return Response(device);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Device")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateDeviceAsync([FromBody] CreateDeviceViewModel device)
    {
        var deviceId = await _deviceService.CreateDeviceAsync(device);
        return Response(deviceId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Device")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateDeviceViewModel>))]
    public async Task<IActionResult> UpdateDeviceAsync([FromBody] UpdateDeviceViewModel device)
    {
        await _deviceService.UpdateDeviceAsync(device);
        return Response(device);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Device")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteDeviceAsync([FromRoute] Guid id)
    {
        await _deviceService.DeleteDeviceAsync(id);
        return Response(id);
    }
}