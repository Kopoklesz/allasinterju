using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Algorithmconstraint
{
    public int Id { get; set; }

    public int Algorithmid { get; set; }

    public string Constraint { get; set; } = null!;

    public virtual Algorithm Algorithm { get; set; } = null!;
}
