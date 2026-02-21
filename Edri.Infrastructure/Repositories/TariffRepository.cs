using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class TariffRepository : BaseRepository<Tariff>, ITariffRepository
{
    public TariffRepository(ApplicationDbContext context) : base(context)
    {
    }
}