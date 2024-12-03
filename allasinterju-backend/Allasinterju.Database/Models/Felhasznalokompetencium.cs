using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Felhasznalokompetencium
{
    public int Id { get; set; }

    public int Kompetenciaid { get; set; }

<<<<<<< HEAD
    public int Felhasznaloid { get; set; }
=======
    public string? Szint { get; set; }

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;
>>>>>>> cbc6b116792ec3f21801ce54873d1174bafdb0fa

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;

    public virtual Kompetencium Kompetencia { get; set; } = null!;
}
