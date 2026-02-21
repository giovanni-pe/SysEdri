using System;

namespace Edri.Shared.Events.User;

public sealed class PasswordChangedEvent : DomainEvent
{
    public PasswordChangedEvent(Guid userId) : base(userId)
    {
    }
}