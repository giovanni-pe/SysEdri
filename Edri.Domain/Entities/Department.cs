using System;
using System.Collections.Generic;

namespace Edri.Domain.Entities;

public class Department : Entity
{
    public string Name { get; private set; }
    public string Code { get; private set; } 


    public virtual ICollection<Province> Provinces { get; private set; }


    public Department(Guid id, string name, string code) : base(id)
    {
        Name = name;
        Code = code;
        Provinces = new List<Province>();
    }

    public void Update(string name, string code)
    {
        Name = name;
        Code = code;
    }
}