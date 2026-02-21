using Edri.gRPC.Interfaces;

namespace Edri.gRPC;

public sealed class Edri : IEdri
{
    public Edri(
        IUsersContext users,
        ITenantsContext tenants)
    {
        Users = users;
        Tenants = tenants;
    }

    public IUsersContext Users { get; }

    public ITenantsContext Tenants { get; }
}