using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Valasz
{
    public int Id { get; set; }

    public int Szoveg { get; set; }

    public int Kerdesid { get; set; }

    public int? Helyes { get; set; }

    public int? Pontszam { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual ICollection<Kitoltottvalasz> Kitoltottvalaszs { get; set; } = new List<Kitoltottvalasz>();
}
