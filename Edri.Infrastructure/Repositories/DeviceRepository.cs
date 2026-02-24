using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class DeviceRepository : BaseRepository<Device>, IDeviceRepository
{
    public DeviceRepository(ApplicationDbContext context) : base(context)
    {
    }
}