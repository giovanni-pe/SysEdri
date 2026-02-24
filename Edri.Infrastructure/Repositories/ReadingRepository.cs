using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class ReadingRepository : BaseRepository<Reading>, IReadingRepository
{
    public ReadingRepository(ApplicationDbContext context) : base(context)
    {
    }
}