using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopscomponent
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Configuration { get; set; }

    public virtual Devop Devops { get; set; } = null!;
}
