using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Edri.Shared.Tenants;

namespace Edri.gRPC.Interfaces;

public interface ITenantsContext
{
    Task<IEnumerable<TenantViewModel>> GetTenantsByIds(IEnumerable<Guid> ids);
}