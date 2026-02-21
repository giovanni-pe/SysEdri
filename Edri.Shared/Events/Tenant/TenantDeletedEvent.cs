using System;

namespace Edri.Shared.Events.Tenant;

public sealed class TenantDeletedEvent : DomainEvent
{
    public TenantDeletedEvent(Guid tenantId) : base(tenantId)
    {
    }
}