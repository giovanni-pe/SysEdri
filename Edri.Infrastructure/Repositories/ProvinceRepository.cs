using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class ProvinceRepository : BaseRepository<Province>, IProvinceRepository
{
    public ProvinceRepository(ApplicationDbContext context) : base(context)
    {
    }
}