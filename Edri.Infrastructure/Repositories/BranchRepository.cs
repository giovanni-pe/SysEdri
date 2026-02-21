using Edri.Domain.Entities;
using Edri.Domain.Interfaces.Repositories;
using Edri.Infrastructure.Database;

namespace Edri.Infrastructure.Repositories;

public sealed class BranchRepository : BaseRepository<Branch>, IBranchRepository
{
    public BranchRepository(ApplicationDbContext context) : base(context)
    {
    }
}