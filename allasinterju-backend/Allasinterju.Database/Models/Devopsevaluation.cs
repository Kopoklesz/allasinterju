using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopsevaluation
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string? Criterion { get; set; }

    public double? Weight { get; set; }

    public string? Desc { get; set; }

    public virtual Devop Devops { get; set; } = null!;
}
