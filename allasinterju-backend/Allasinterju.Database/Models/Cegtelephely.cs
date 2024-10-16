using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Cegtelephely
{
    public int Id { get; set; }

    public string Irsz { get; set; } = null!;

    public string Telepules { get; set; } = null!;

    public string Utcahazszam { get; set; } = null!;

    public int? Cegid { get; set; }

    public virtual ICollection<Allas> Allas { get; set; } = new List<Allas>();

    public virtual Ceg? Ceg { get; set; }

    public virtual ICollection<Ceg> Cegs { get; set; } = new List<Ceg>();
}
