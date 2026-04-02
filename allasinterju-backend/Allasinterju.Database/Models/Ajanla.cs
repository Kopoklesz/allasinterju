using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Ajanla
{
    public int Id { get; set; }

    public int Allasid { get; set; }

    public int Allaskeresoid { get; set; }

    public bool Jelentkezve { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual Felhasznalo Allaskereso { get; set; } = null!;
}
