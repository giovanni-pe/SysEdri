using System;
using System.Collections.Generic;
using System.Linq;
using Edri.Application.ViewModels.Users;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Tenants;

public sealed class TenantViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();

    public static TenantViewModel FromTenant(Tenant tenant)
    {
        return new TenantViewModel
        {
            Id = tenant.Id,
            Name = tenant.Name,
            Users = tenant.Users.Select(UserViewModel.FromUser).ToList()
        };
    }
}