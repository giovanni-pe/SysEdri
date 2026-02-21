using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Districts;

public sealed class DistrictViewModel
{
    public Guid Id { get; set; }

    public Guid ProvinceId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Campo agregado

    public static DistrictViewModel FromDistrict(District District)
    {
        return new DistrictViewModel
        {
            Id = District.Id,
            ProvinceId = District.ProvinceId,
            Name = District.Name,
            Code = District.Code
        };
    }
}