using System;
using System.Collections.Generic;

namespace Edri.Domain.Entities;

public class Province : Entity
{
    public string Name { get; private set; }
    public string Code { get; private set; } // Ej: "01" (Provincia Lima)

    // Foreign Key
    public Guid DepartmentId { get; private set; }
    public virtual Department Department { get; private set; } = null!;

    // Relación con Distritos
    public virtual ICollection<District> Districts { get; private set; }

    public Province(Guid id, Guid departmentId, string name, string code) : base(id)
    {
        DepartmentId = departmentId;
        Name = name;
        Code = code;
    }

    public void Update( Guid departmentId,string name, string code)
    {
        Name = name;
        Code = code;
        DepartmentId = departmentId;
    }
}