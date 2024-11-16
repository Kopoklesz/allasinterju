using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Felhasznalokompetencium
{
    public int Id { get; set; }

    public int Kompetenciaid { get; set; }

    public int Felhasznaloid { get; set; }

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;

    public virtual Kompetencium Kompetencia { get; set; } = null!;
}
