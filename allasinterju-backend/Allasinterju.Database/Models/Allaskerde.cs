using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allaskerde
{
    public int Id { get; set; }

    public int Allasid { get; set; }

    public int Kerdesid { get; set; }

    public int? Sorszam { get; set; }

    public int Kor { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual Kerde Kerdes { get; set; } = null!;
}
