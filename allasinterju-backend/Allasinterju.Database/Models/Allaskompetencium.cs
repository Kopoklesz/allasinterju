using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allaskompetencium
{
    public int Id { get; set; }

    public int Allasid { get; set; }

    public int Kompetenciaid { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual Kompetencium Kompetencia { get; set; } = null!;
}
