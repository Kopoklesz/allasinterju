using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allaskerdoiv
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public int Kerdesid { get; set; }

    public int? Sorszam { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;
}
