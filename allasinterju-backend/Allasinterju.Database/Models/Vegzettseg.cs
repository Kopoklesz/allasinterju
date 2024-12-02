using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Vegzettseg
{
    public int Id { get; set; }

    public int Felhasznaloid { get; set; }

    public string Rovidleiras { get; set; } = null!;

    public string? Hosszuleiras { get; set; }

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;
}
