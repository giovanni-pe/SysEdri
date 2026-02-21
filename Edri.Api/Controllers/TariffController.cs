using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Tariffs;
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
public sealed class TariffController : ApiController
{
    private readonly ITariffService _tariffService;

    public TariffController(
        INotificationHandler<DomainNotification> notifications,
        ITariffService tariffService) : base(notifications)
    {
        _tariffService = tariffService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Tariffs")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<TariffViewModel>>))]
    public async Task<IActionResult> GetAllTariffsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<TariffViewModelSortProvider, TariffViewModel, Tariff>]
        SortQuery? sortQuery = null)
    {
        var tariffs = await _tariffService.GetAllTariffsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(tariffs);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Tariff by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<TariffViewModel>))]
    public async Task<IActionResult> GetTariffByIdAsync([FromRoute] Guid id)
    {
        var tariff = await _tariffService.GetTariffByIdAsync(id);
        return Response(tariff);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Tariff")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateTariffAsync([FromBody] CreateTariffViewModel tariff)
    {
        var tariffId = await _tariffService.CreateTariffAsync(tariff);
        return Response(tariffId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Tariff")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateTariffViewModel>))]
    public async Task<IActionResult> UpdateTariffAsync([FromBody] UpdateTariffViewModel tariff)
    {
        await _tariffService.UpdateTariffAsync(tariff);
        return Response(tariff);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Tariff")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteTariffAsync([FromRoute] Guid id)
    {
        await _tariffService.DeleteTariffAsync(id);
        return Response(id);
    }
}