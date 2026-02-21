using Edri.gRPC.Contexts;
using Edri.gRPC.Interfaces;
using Edri.gRPC.Models;
using Edri.Proto.Tenants;
using Edri.Proto.Users;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Edri.gRPC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGrpcClient(
        this IServiceCollection services,
        IConfiguration configuration,
        string configSectionKey = "gRPC")
    {
        var settings = new GRPCSettings();
        configuration.Bind(configSectionKey, settings);

        return AddGrpcClient(services, settings);
    }

    public static IServiceCollection AddGrpcClient(this IServiceCollection services, GRPCSettings settings)
    {
        if (!string.IsNullOrWhiteSpace(settings.EdriUrl))
        {
            services.AddEdriGrpcClient(settings.EdriUrl);
        }

        services.AddSingleton<IEdri, Edri>();

        return services;
    }

    public static IServiceCollection AddEdriGrpcClient(
        this IServiceCollection services,
        string gRPCUrl)
    {
        if (string.IsNullOrWhiteSpace(gRPCUrl))
        {
            return services;
        }

        var channel = GrpcChannel.ForAddress(gRPCUrl);

        var usersClient = new UsersApi.UsersApiClient(channel);
        services.AddSingleton(usersClient);

        var tenantsClient = new TenantsApi.TenantsApiClient(channel);
        services.AddSingleton(tenantsClient);

        services.AddSingleton<IUsersContext, UsersContext>();
        services.AddSingleton<ITenantsContext, TenantsContext>();

        return services;
    }
}