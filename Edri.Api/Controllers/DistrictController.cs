using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Districts;
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
public sealed class DistrictController : ApiController
{
    private readonly IDistrictService _DistrictService;

    public DistrictController(
        INotificationHandler<DomainNotification> notifications,
        IDistrictService DistrictService) : base(notifications)
    {
        _DistrictService = DistrictService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Districts")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DistrictViewModel>>))]
    public async Task<IActionResult> GetAllDistrictsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<DistrictViewModelSortProvider, DistrictViewModel, District>]
        SortQuery? sortQuery = null)
    {
        var Districts = await _DistrictService.GetAllDistrictsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Districts);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a District by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DistrictViewModel>))]
    public async Task<IActionResult> GetDistrictByIdAsync([FromRoute] Guid id)
    {
        var District = await _DistrictService.GetDistrictByIdAsync(id);
        return Response(District);
    }

    [HttpPost]
    [SwaggerOperation("Create a new District")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateDistrictAsync([FromBody] CreateDistrictViewModel District)
    {
        var DistrictId = await _DistrictService.CreateDistrictAsync(District);
        return Response(DistrictId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing District")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateDistrictViewModel>))]
    public async Task<IActionResult> UpdateDistrictAsync([FromBody] UpdateDistrictViewModel District)
    {
        await _DistrictService.UpdateDistrictAsync(District);
        return Response(District);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing District")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteDistrictAsync([FromRoute] Guid id)
    {
        await _DistrictService.DeleteDistrictAsync(id);
        return Response(id);
    }
}