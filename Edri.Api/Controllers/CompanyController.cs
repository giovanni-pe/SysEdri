using System;
using System.Threading.Tasks;
using Edri.Api.Models;
using Edri.Api.Swagger;
using Edri.Application.Interfaces;
using Edri.Application.SortProviders;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;
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
public sealed class CompanyController : ApiController
{
    private readonly ICompanyService _companyService;

    public CompanyController(
        INotificationHandler<DomainNotification> notifications,
        ICompanyService companyService) : base(notifications)
    {
        _companyService = companyService;
    }

    [HttpGet]
    [SwaggerOperation("Get a list of all Companies")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<PagedResult<CompanyViewModel>>))]
    public async Task<IActionResult> GetAllCompaniesAsync(
        [FromQuery] PageQuery query,
        [FromQuery] string searchTerm = "",
        [FromQuery] bool includeDeleted = false,
        [FromQuery] [SortableFieldsAttribute<CompanyViewModelSortProvider, CompanyViewModel, Company>]
        SortQuery? sortQuery = null)
    {
        var companies = await _companyService.GetAllCompaniesAsync(
            query,
            includeDeleted,
            searchTerm,
            sortQuery);

        return Response(companies);
    }

    [HttpGet("{id}")]
    [SwaggerOperation("Get a Company by id")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<CompanyViewModel>))]
    public async Task<IActionResult> GetCompanyByIdAsync([FromRoute] Guid id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);
        return Response(company);
    }

    [HttpPost]
    [SwaggerOperation("Create a new Company")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> CreateCompanyAsync([FromBody] CreateCompanyViewModel company)
    {
        var companyId = await _companyService.CreateCompanyAsync(company);
        return Response(companyId);
    }

    [HttpPut]
    [SwaggerOperation("Update an existing Company")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<UpdateCompanyViewModel>))]
    public async Task<IActionResult> UpdateCompanyAsync([FromBody] UpdateCompanyViewModel company)
    {
        await _companyService.UpdateCompanyAsync(company);
        return Response(company);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete an existing Company")]
    [SwaggerResponse(200, "Request successful", typeof(ResponseMessage<Guid>))]
    public async Task<IActionResult> DeleteCompanyAsync([FromRoute] Guid id)
    {
        await _companyService.DeleteCompanyAsync(id);
        return Response(id);
    }
}