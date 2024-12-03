using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testingevaluation
{
    public int Id { get; set; }

    public int Testingid { get; set; }

    public string? Criterion { get; set; }

    public double? Weight { get; set; }

    public string? Desc { get; set; }

    public virtual Testing Testing { get; set; } = null!;
}
