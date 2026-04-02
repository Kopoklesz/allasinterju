using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testingtool
{
    public int Id { get; set; }

    public int Testingid { get; set; }

    public string Name { get; set; } = null!;

    public string? Version { get; set; }

    public string? Purpose { get; set; }

    public virtual Testing Testing { get; set; } = null!;
}
