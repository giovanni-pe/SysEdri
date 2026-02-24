using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Captures;
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
public sealed class CaptureController : ApiController
{
    private readonly ICaptureService _captureService;

    public CaptureController(
        INotificationHandler<DomainNotification> notifications,
        ICaptureService captureService) : base(notifications)
    {
        _captureService = captureService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Captures")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CaptureViewModel>>))]
    public async Task<IActionResult> GetAllCapturesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<CaptureViewModelSortProvider, CaptureViewModel, Capture>]
        SortQuery? sortQuery = null)
    {
        var captures = await _captureService.GetAllCapturesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(captures);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Capture by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CaptureViewModel>))]
    public async Task<IActionResult> GetCaptureByIdAsync([FromRoute] Guid id)
    {
        var capture = await _captureService.GetCaptureByIdAsync(id);
        return Response(capture);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Capture")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateCaptureAsync([FromBody] CreateCaptureViewModel capture)
    {
        var captureId = await _captureService.CreateCaptureAsync(capture);
        return Response(captureId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Capture")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCaptureViewModel>))]
    public async Task<IActionResult> UpdateCaptureAsync([FromBody] UpdateCaptureViewModel capture)
    {
        await _captureService.UpdateCaptureAsync(capture);
        return Response(capture);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Capture")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteCaptureAsync([FromRoute] Guid id)
    {
        await _captureService.DeleteCaptureAsync(id);
        return Response(id);
    }
}