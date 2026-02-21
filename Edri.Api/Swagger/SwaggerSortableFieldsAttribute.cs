using System;
using System.Collections.Generic;

namespace Edri.Api.Swagger;

public abstract class SwaggerSortableFieldsAttribute : Attribute
{
    public abstract IEnumerable<string> GetFields();
}