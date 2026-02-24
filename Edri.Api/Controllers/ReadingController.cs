using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Readings;
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
public sealed class ReadingController : ApiController
{
    private readonly IReadingService _readingService;

    public ReadingController(
        INotificationHandler<DomainNotification> notifications,
        IReadingService readingService) : base(notifications)
    {
        _readingService = readingService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Readings")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ReadingViewModel>>))]
    public async Task<IActionResult> GetAllReadingsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ReadingViewModelSortProvider, ReadingViewModel, Reading>]
        SortQuery? sortQuery = null)
    {
        var readings = await _readingService.GetAllReadingsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(readings);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Reading by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ReadingViewModel>))]
    public async Task<IActionResult> GetReadingByIdAsync([FromRoute] Guid id)
    {
        var reading = await _readingService.GetReadingByIdAsync(id);
        return Response(reading);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Reading")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateReadingAsync([FromBody] CreateReadingViewModel reading)
    {
        var readingId = await _readingService.CreateReadingAsync(reading);
        return Response(readingId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Reading")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateReadingViewModel>))]
    public async Task<IActionResult> UpdateReadingAsync([FromBody] UpdateReadingViewModel reading)
    {
        await _readingService.UpdateReadingAsync(reading);
        return Response(reading);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Reading")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteReadingAsync([FromRoute] Guid id)
    {
        await _readingService.DeleteReadingAsync(id);
        return Response(id);
    }
}