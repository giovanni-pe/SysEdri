using System;
using Edri.Domain.Commands.Tenants.CreateTenant;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces.Repositories;
using NSubstitute;

namespace Edri.Domain.Tests.CommandHandler.Tenant.CreateTenant;

public sealed class CreateTenantCommandTestFixture : CommandHandlerFixtureBase
{
    public CreateTenantCommandHandler CommandHandler { get; }

    private ITenantRepository TenantRepository { get; }

    public CreateTenantCommandTestFixture()
    {
        TenantRepository = Substitute.For<ITenantRepository>();

        CommandHandler = new CreateTenantCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            TenantRepository,
            User);
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }

    public void SetupExistingTenant(Guid id)
    {
        TenantRepository
            .ExistsAsync(Arg.Is<Guid>(x => x == id))
            .Returns(true);
    }
}