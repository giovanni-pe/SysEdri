using System;
using FluentValidation.Results;
using MediatR;

namespace Edri.Domain.Commands;

public abstract class CommandBase : IRequest
{
    public Guid AggregateId { get; }
    public string MessageType { get; }
    public DateTime Timestamp { get; }
    public ValidationResult? ValidationResult { get; protected set; }

    protected CommandBase(Guid aggregateId)
    {
        MessageType = GetType().Name;

        // CORRECCIÓN: Usar UtcNow para compatibilidad con PostgreSQL
        Timestamp = DateTime.UtcNow;

        AggregateId = aggregateId;
    }

    public abstract bool IsValid();
}