using System.Threading.Tasks;
using Edri.Domain.Commands;
using Edri.Shared.Events;
using MediatR;

namespace Edri.Domain.Interfaces;

public interface IMediatorHandler
{
    Task RaiseEventAsync<T>(T @event) where T : DomainEvent;

    Task SendCommandAsync<T>(T command) where T : CommandBase;

    Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query);
}