using System;

namespace Edri.Domain.Entities;

public class District : Entity
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public Guid ProvinceId { get; private set; }
    public virtual Province Province { get; private set; } = null!;


    public District(Guid id, Guid provinceId, string name, string code) : base(id)
    {
        ProvinceId = provinceId;
        Name = name;
        Code = code;
    }

    public void Update( Guid provinceId,string name, string code)
    {
        Name = name;
        Code = code;
        ProvinceId = provinceId;
    }
}