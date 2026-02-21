using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Edri.Shared.Users;

namespace Edri.gRPC.Interfaces;

public interface IUsersContext
{
    Task<IEnumerable<UserViewModel>> GetUsersByIds(IEnumerable<Guid> ids);
}