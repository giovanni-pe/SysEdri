using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class DistrictRepository : BaseRepository<District>, IDistrictRepository
{
    public DistrictRepository(ApplicationDbContext context) : base(context)
    {
    }
}