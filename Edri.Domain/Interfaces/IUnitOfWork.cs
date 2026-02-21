using System;
using System.Threading.Tasks;

namespace Edri.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public Task<bool> CommitAsync();
}