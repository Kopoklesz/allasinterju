using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kompetencium
{
    public int Id { get; set; }

    public string Tipus { get; set; } = null!;

    public string Leiras { get; set; } = null!;

    public virtual ICollection<Felhasznalokompetencium> Felhasznalokompetencia { get; set; } = new List<Felhasznalokompetencium>();
}
