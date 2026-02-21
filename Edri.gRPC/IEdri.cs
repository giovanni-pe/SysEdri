using Edri.gRPC.Interfaces;

namespace Edri.gRPC;

public interface IEdri
{
    IUsersContext Users { get; }
    ITenantsContext Tenants { get; }
}