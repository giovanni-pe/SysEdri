using System;
using System.Net.Http;
using Edri.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.IntegrationTests.Fixtures;

public class TestFixtureBase
{
    public HttpClient ServerClient { get; }
    protected EdriWebApplicationFactory Factory { get; }

    public TestFixtureBase(bool useTestAuthentication = true)
    {
        Factory = new EdriWebApplicationFactory(
            RegisterCustomServicesHandler,
            useTestAuthentication);

        ServerClient = Factory.CreateClient();
        ServerClient.Timeout = TimeSpan.FromMinutes(5);
    }

    protected virtual void RegisterCustomServicesHandler(
        IServiceCollection services)
    {
    }
}