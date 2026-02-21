using System;
using Edri.Domain.Entities;

namespace Edri.Application.ViewModels.Departments;

public sealed class DepartmentViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty; // Campo agregado

    public static DepartmentViewModel FromDepartment(Department department)
    {
        return new DepartmentViewModel
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code
        };
    }
}