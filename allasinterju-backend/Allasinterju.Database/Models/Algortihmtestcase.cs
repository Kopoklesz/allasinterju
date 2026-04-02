using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Algortihmtestcase
{
    public int Id { get; set; }

    public int Algorithmid { get; set; }

    public string? Input { get; set; }

    public string? Output { get; set; }

    public bool Hidden { get; set; }

    public int? Points { get; set; }

    public virtual Algorithm Algorithm { get; set; } = null!;
}
