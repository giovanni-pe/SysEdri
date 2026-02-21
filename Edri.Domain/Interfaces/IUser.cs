using System;
using Edri.Domain.Enums;

namespace Edri.Domain.Interfaces;

public interface IUser
{
    string Name { get; }
    Guid GetUserId();
    UserRole GetUserRole();
    string GetUserEmail();
}