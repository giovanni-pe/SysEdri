using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class MeterRepository : BaseRepository<Meter>, IMeterRepository
{
    public MeterRepository(ApplicationDbContext context) : base(context)
    {
    }
}