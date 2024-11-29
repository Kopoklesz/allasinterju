using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allaskapcsolattarto
{
    public int Id { get; set; }

    public int Allasid { get; set; }

    public int Kapcsolattartoid { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual Felhasznalo Kapcsolattarto { get; set; } = null!;
}
