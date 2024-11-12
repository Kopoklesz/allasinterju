using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allasvizsgalo
{
    public int Id { get; set; }

    public int Allasid { get; set; }

    public int Felhasznaloid { get; set; }

    public virtual Allas Allas { get; set; } = null!;

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;
}
