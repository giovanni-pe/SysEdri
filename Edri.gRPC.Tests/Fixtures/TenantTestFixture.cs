using System;
using System.Collections.Generic;
using Edri.Application.gRPC;
using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using MockQueryable;
using NSubstitute;

namespace Edri.gRPC.Tests.Fixtures;

public sealed class TenantTestFixture
{
    public TenantsApiImplementation TenantsApiImplementation { get; }
    private ITenantRepository TenantRepository { get; }

    public IList<Tenant> ExistingTenants { get; }

    public TenantTestFixture()
    {
        TenantRepository = Substitute.For<ITenantRepository>();

        ExistingTenants = new List<Tenant>
        {
            new(Guid.NewGuid(), "Tenant 1"),
            new(Guid.NewGuid(), "Tenant 2"),
            new(Guid.NewGuid(), "Tenant 3")
        };

        TenantRepository.GetAllNoTracking().Returns(ExistingTenants.BuildMock());

        TenantsApiImplementation = new TenantsApiImplementation(TenantRepository);
    }
}