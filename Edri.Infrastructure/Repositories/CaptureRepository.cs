using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class CaptureRepository : BaseRepository<Capture>, ICaptureRepository
{
    public CaptureRepository(ApplicationDbContext context) : base(context)
    {
    }
}