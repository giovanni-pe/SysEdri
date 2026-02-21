using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Departments;
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
public sealed class DepartmentController : ApiController
{
    private readonly IDepartmentService _DepartmentService;

    public DepartmentController(
        INotificationHandler<DomainNotification> notifications,
        IDepartmentService DepartmentService) : base(notifications)
    {
        _DepartmentService = DepartmentService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Departments")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<DepartmentViewModel>>))]
    public async Task<IActionResult> GetAllDepartmentsAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<DepartmentViewModelSortProvider, DepartmentViewModel, Department>]
        SortQuery? sortQuery = null)
    {
        var Departments = await _DepartmentService.GetAllDepartmentsAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);
        return Response(Departments);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Department by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<DepartmentViewModel>))]
    public async Task<IActionResult> GetDepartmentByIdAsync([FromRoute] Guid id)
    {
        var Department = await _DepartmentService.GetDepartmentByIdAsync(id);
        return Response(Department);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Department")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateDepartmentAsync([FromBody] CreateDepartmentViewModel Department)
    {
        var DepartmentId = await _DepartmentService.CreateDepartmentAsync(Department);
        return Response(DepartmentId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Department")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateDepartmentViewModel>))]
    public async Task<IActionResult> UpdateDepartmentAsync([FromBody] UpdateDepartmentViewModel Department)
    {
        await _DepartmentService.UpdateDepartmentAsync(Department);
        return Response(Department);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Department")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteDepartmentAsync([FromRoute] Guid id)
    {
        await _DepartmentService.DeleteDepartmentAsync(id);
        return Response(id);
    }
}