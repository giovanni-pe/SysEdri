using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Provinces;

public sealed class ProvinceViewModel
{
    public Guid Id { get; set; }

    public Guid DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Campo agregado

    public static ProvinceViewModel FromProvince(Province Province)
    {
        return new ProvinceViewModel
        {
            Id = Province.Id,
            DepartmentId = Province.DepartmentId,
            Name = Province.Name,
            Code = Province.Code
        };
    }
}