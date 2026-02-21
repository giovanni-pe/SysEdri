using System.Threading.Tasks;
using Edri.Domain.Entities;

namespace Edri.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}