using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Edri.Application.Extensions;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Edri.Application.Queries.Companies.GetAll;

public sealed class GetAllCompaniesQueryHandler :
    IRequestHandler<GetAllCompaniesQuery, PagedResult<CompanyViewModel>>
{
    private readonly ISortingExpressionProvider<CompanyViewModel, Company> _sortingExpressionProvider;
    private readonly ICompanyRepository _companyRepository;

    public GetAllCompaniesQueryHandler(
        ICompanyRepository companyRepository,
        ISortingExpressionProvider<CompanyViewModel, Company> sortingExpressionProvider)
    {
        _companyRepository = companyRepository;
        _sortingExpressionProvider = sortingExpressionProvider;
    }

    public async Task<PagedResult<CompanyViewModel>> Handle(
        GetAllCompaniesQuery request,
        CancellationToken cancellationToken)
    {
        var companiesQuery = _companyRepository
            .GetAllNoTracking()
            .IgnoreQueryFilters()
            .Where(x => request.IncludeDeleted || x.DeletedAt == null);

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            companiesQuery = companiesQuery.Where(company =>
                company.TaxId.Contains(request.SearchTerm) ||
                company.BusinessName.Contains(request.SearchTerm) ||
                (company.TradeName != null && company.TradeName.Contains(request.SearchTerm)));
        }

        var totalCount = await companiesQuery.CountAsync(cancellationToken);

        companiesQuery = companiesQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

        var companies = await companiesQuery
            .Skip((request.Query.Page - 1) * request.Query.PageSize)
            .Take(request.Query.PageSize)
            .Select(company => CompanyViewModel.FromCompany(company))
            .ToListAsync(cancellationToken);

        return new PagedResult<CompanyViewModel>(
            totalCount, companies, request.Query.Page, request.Query.PageSize);
    }
}