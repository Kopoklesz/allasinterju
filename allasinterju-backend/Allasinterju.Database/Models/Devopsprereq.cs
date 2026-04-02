using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopsprereq
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string Tool { get; set; } = null!;

    public string? Version { get; set; }

    public string? Purpose { get; set; }

    public virtual Devop Devops { get; set; } = null!;
}
