using System;
using System.Collections.Generic;
using Edri.Application.Queries.Tenants.GetAll;
using Edri.Application.SortProviders;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MockQueryable;
using NSubstitute;

namespace Edri.Application.Tests.Fixtures.Queries.Tenants;

public sealed class GetAllTenantsTestFixture : QueryHandlerBaseFixture
{
    public GetAllTenantsQueryHandler QueryHandler { get; }
    private ITenantRepository TenantRepository { get; }

    public GetAllTenantsTestFixture()
    {
        TenantRepository = Substitute.For<ITenantRepository>();
        var sortingProvider = new TenantViewModelSortProvider();

        QueryHandler = new GetAllTenantsQueryHandler(TenantRepository, sortingProvider);
    }

    public Tenant SetupTenant(bool deleted = false)
    {
        var tenant = new Tenant(Guid.NewGuid(), "Tenant 1");

        if (deleted)
        {
            tenant.Delete();
        }

        var tenantList = new List<Tenant> { tenant }.BuildMock();
        TenantRepository.GetAllNoTracking().Returns(tenantList);

        return tenant;
    }
}