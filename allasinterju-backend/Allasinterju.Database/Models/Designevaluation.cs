using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Designevaluation
{
    public int Id { get; set; }

    public int Designid { get; set; }

    public string? Description { get; set; }

    public double? Weight { get; set; }

    public virtual Design Design { get; set; } = null!;
}
