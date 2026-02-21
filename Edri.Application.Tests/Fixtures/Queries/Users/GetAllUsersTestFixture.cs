using System;
using Edri.Application.Queries.Users.GetAll;
using Edri.Application.SortProviders;
using Edri.Domain.Entities;
using Edri.Domain.Enums;
using Edri.Domain.Interfaces.Repositories;
using MockQueryable;
using NSubstitute;

namespace Edri.Application.Tests.Fixtures.Queries.Users;

public sealed class GetAllUsersTestFixture : QueryHandlerBaseFixture
{
    private IUserRepository UserRepository { get; }
    public GetAllUsersQueryHandler Handler { get; }
    public Guid ExistingUserId { get; } = Guid.NewGuid();

    public GetAllUsersTestFixture()
    {
        UserRepository = Substitute.For<IUserRepository>();
        var sortingProvider = new UserViewModelSortProvider();

        Handler = new GetAllUsersQueryHandler(UserRepository, sortingProvider);
    }

    public User SetupUserAsync()
    {
        var user = new User(
            ExistingUserId,
            Guid.NewGuid(),
            "max@mustermann.com",
            "Max",
            "Mustermann",
            "Password",
            UserRole.User);

        var query = new[] { user }.BuildMock();

        UserRepository.GetAllNoTracking().Returns(query);

        return user;
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