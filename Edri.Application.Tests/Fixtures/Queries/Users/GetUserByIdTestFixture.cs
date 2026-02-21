using System;
using System.Linq;
using Edri.Application.Queries.Users.GetUserById;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces.Repositories;
using MockQueryable;
using NSubstitute;

namespace Edri.Application.Tests.Fixtures.Queries.Users;

public sealed class GetUserByIdTestFixture : QueryHandlerBaseFixture
{
    private IUserRepository UserRepository { get; }
    public GetUserByIdQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();

    public GetUserByIdTestFixture()
    {
        UserRepository = Substitute.For<IUserRepository>();

        Handler = new GetUserByIdQueryHandler(UserRepository, Bus);
    }

    public void SetupUserAsync()
    {
        var user = new User(
            ExistingUserId,
            Guid.NewGuid(),
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "Password",
            UserRole.User);

        UserRepository.GetByIdAsync(Arg.Is<Guid>(y => y == ExistingUserId)).Returns(user);
    }

    public void SetupDeletedUserAsync()
    {
        var user = new User(
            ExistingUserId,
            Guid.NewGuid(),
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "Password",
            UserRole.User);

        user.Delete();

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);
    }
}