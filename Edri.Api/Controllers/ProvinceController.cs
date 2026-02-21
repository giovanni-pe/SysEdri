using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Provinces;
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
public sealed class ProvinceController : ApiController
{
    private readonly IProvinceService _ProvinceService;

    public ProvinceController(
        INotificationHandler<DomainNotification> notifications,
        IProvinceService ProvinceService) : base(notifications)
    {
        _ProvinceService = ProvinceService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Provinces")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<ProvinceViewModel>>))]
    public async Task<IActionResult> GetAllProvincesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<ProvinceViewModelSortProvider, ProvinceViewModel, Province>]
        SortQuery? sortQuery = null)
    {
        var Provinces = await _ProvinceService.GetAllProvincesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Provinces);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Province by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<ProvinceViewModel>))]
    public async Task<IActionResult> GetProvinceByIdAsync([FromRoute] Guid id)
    {
        var Province = await _ProvinceService.GetProvinceByIdAsync(id);
        return Response(Province);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Province")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateProvinceAsync([FromBody] CreateProvinceViewModel Province)
    {
        var ProvinceId = await _ProvinceService.CreateProvinceAsync(Province);
        return Response(ProvinceId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Province")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateProvinceViewModel>))]
    public async Task<IActionResult> UpdateProvinceAsync([FromBody] UpdateProvinceViewModel Province)
    {
        await _ProvinceService.UpdateProvinceAsync(Province);
        return Response(Province);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Province")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteProvinceAsync([FromRoute] Guid id)
    {
        await _ProvinceService.DeleteProvinceAsync(id);
        return Response(id);
    }
}