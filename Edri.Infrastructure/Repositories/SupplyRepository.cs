using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class SupplyRepository : BaseRepository<Supply>, ISupplyRepository
{
    public SupplyRepository(ApplicationDbContext context) : base(context)
    {
    }
}