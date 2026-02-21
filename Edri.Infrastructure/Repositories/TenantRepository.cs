using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class TenantRepository : BaseRepository<Tenant>, ITenantRepository
{
    public TenantRepository(ApplicationDbContext context) : base(context)
    {
    }
}