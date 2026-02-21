using System.Threading.Tasks;
using Edri.Shared.Events;

namespace Edri.Domain.DomainEvents;

public interface IDomainEventStore
{
    Task SaveAsync<T>(T domainEvent) where T : DomainEvent;
}