using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Teszteset
{
    public int Id { get; set; }

    public int Kerdesid { get; set; }

    public string? Bemenet { get; set; }

    public string? Kimenet { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual ICollection<Lefutottteszteset> Lefutotttesztesets { get; set; } = new List<Lefutottteszteset>();
}
