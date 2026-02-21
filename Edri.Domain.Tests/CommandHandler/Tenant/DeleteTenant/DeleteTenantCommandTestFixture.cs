using System;
using Edri.Domain.Commands.Tenants.DeleteTenant;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces.Repositories;
using NSubstitute;

namespace Edri.Domain.Tests.CommandHandler.Tenant.DeleteTenant;

public sealed class DeleteTenantCommandTestFixture : CommandHandlerFixtureBase
{
    public DeleteTenantCommandHandler CommandHandler { get; }

    private ITenantRepository TenantRepository { get; }
    private IUserRepository UserRepository { get; }

    public DeleteTenantCommandTestFixture()
    {
        TenantRepository = Substitute.For<ITenantRepository>();
        UserRepository = Substitute.For<IUserRepository>();

        CommandHandler = new DeleteTenantCommandHandler(
            Bus,
            UnitOfWork,
            NotificationHandler,
            TenantRepository,
            UserRepository,
            User);
    }

    public Entities.Tenant SetupTenant()
    {
        var tenant = new Entities.Tenant(Guid.NewGuid(), "TestTenant");

        TenantRepository
            .GetByIdAsync(Arg.Is<Guid>(y => y == tenant.Id))
            .Returns(tenant);

        return tenant;
    }

    public void SetupUser()
    {
        User.GetUserRole().Returns(UserRole.User);
    }
}