using System.Threading.Tasks;
using Edri.Shared.Events;

namespace Edri.Domain.EventHandler.Fanout;

public interface IFanoutEventHandler
{
    Task<T> HandleDomainEventAsync<T>(T @event) where T : DomainEvent;
}