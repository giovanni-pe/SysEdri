using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Branches;
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
public sealed class BranchController : ApiController
{
    private readonly IBranchService _branchService;

    public BranchController(
        INotificationHandler<DomainNotification> notifications,
        IBranchService branchService) : base(notifications)
    {
        _branchService = branchService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Branches")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<BranchViewModel>>))]
    public async Task<IActionResult> GetAllBranchesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<BranchViewModelSortProvider, BranchViewModel, Branch>]
        SortQuery? sortQuery = null)
    {
        var branches = await _branchService.GetAllBranchesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(branches);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Branch by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<BranchViewModel>))]
    public async Task<IActionResult> GetBranchByIdAsync([FromRoute] Guid id)
    {
        var branch = await _branchService.GetBranchByIdAsync(id);
        return Response(branch);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Branch")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateBranchAsync([FromBody] CreateBranchViewModel branch)
    {
        var branchId = await _branchService.CreateBranchAsync(branch);
        return Response(branchId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Branch")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateBranchViewModel>))]
    public async Task<IActionResult> UpdateBranchAsync([FromBody] UpdateBranchViewModel branch)
    {
        await _branchService.UpdateBranchAsync(branch);
        return Response(branch);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Branch")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteBranchAsync([FromRoute] Guid id)
    {
        await _branchService.DeleteBranchAsync(id);
        return Response(id);
    }
}