using System;
using System.Threading.Tasks;
using Edri.Application.Interfaces;
using Edri.Application.Queries.Companies.GetAll;
using Edri.Application.Queries.Companies.GetCompanyById;
using Edri.Application.ViewModels;
using Edri.Application.ViewModels.Sorting;
using Edri.Application.ViewModels.Companies;
using Edri.Domain;
using Edri.Domain.Commands.Companies.CreateCompany;
using Edri.Domain.Commands.Companies.DeleteCompany;
using Edri.Domain.Commands.Companies.UpdateCompany;
using Edri.Domain.Entities;
using Edri.Domain.Extensions;
using Edri.Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Edri.Application.Services;

public sealed class CompanyService : ICompanyService
{
    private readonly IMediatorHandler _bus;
    private readonly IDistributedCache _distributedCache;

    public CompanyService(IMediatorHandler bus, IDistributedCache distributedCache)
    {
        _bus = bus;
        _distributedCache = distributedCache;
    }

    public async Task<Guid> CreateCompanyAsync(CreateCompanyViewModel company)
    {
        var companyId = Guid.NewGuid();

        await _bus.SendCommandAsync(new CreateCompanyCommand(
            companyId,
            company.TaxId,
            company.BusinessName,
            company.TradeName,
            company.FiscalAddress,
            company.Phone,
            company.Email,
            company.LogoUrl));

        return companyId;
    }

    public async Task UpdateCompanyAsync(UpdateCompanyViewModel company)
    {
        await _bus.SendCommandAsync(new UpdateCompanyCommand(
            company.Id,
            company.TaxId,
            company.BusinessName,
            company.TradeName,
            company.FiscalAddress,
            company.Phone,
            company.Email,
            company.LogoUrl));
    }

    public async Task DeleteCompanyAsync(Guid companyId)
    {
        await _bus.SendCommandAsync(new DeleteCompanyCommand(companyId));
    }

    public async Task<CompanyViewModel?> GetCompanyByIdAsync(Guid companyId)
    {
        var cachedCompany = await _distributedCache.GetOrCreateJsonAsync(
            CacheKeyGenerator.GetEntityCacheKey<Company>(companyId),
            async () => await _bus.QueryAsync(new GetCompanyByIdQuery(companyId)),
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromDays(3),
                AbsoluteExpiration = DateTimeOffset.Now.AddDays(30)
            });

        return cachedCompany;
    }

    public async Task<PagedResult<CompanyViewModel>> GetAllCompaniesAsync(
        PageQuery query,
        bool includeDeleted,
        string searchTerm = "",
        SortQuery? sortQuery = null)
    {
        return await _bus.QueryAsync(new GetAllCompaniesQuery(query, includeDeleted, searchTerm, sortQuery));
    }
}