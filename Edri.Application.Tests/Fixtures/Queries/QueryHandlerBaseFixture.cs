using Edri.Domain.Interfaces;
using Edri.Domain.Notifications;
using NSubstitute;

namespace Edri.Application.Tests.Fixtures.Queries;

public class QueryHandlerBaseFixture
{
    public IMediatorHandler Bus { get; } = Substitute.For<IMediatorHandler>();

    public QueryHandlerBaseFixture VerifyExistingNotification(string key, string errorCode, string message)
    {
        Bus.Received(1).RaiseEventAsync(Arg.Is<DomainNotification>(notification =>
            notification.Key == key &&
            notification.Code == errorCode &&
            notification.Value == message));

        return this;
    }

    public QueryHandlerBaseFixture VerifyNoDomainNotification()
    {
        Bus.DidNotReceive().RaiseEventAsync(Arg.Any<DomainNotification>());

        return this;
    }
}