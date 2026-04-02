using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kerde
{
    public int Id { get; set; }

    public string? Szoveg { get; set; }

    public int? Maxpont { get; set; }

    public string? Programalapszoveg { get; set; }

    public string? Programteszteset { get; set; }

    public int Kerdoivid { get; set; }

    public int? Sorrendkerdes { get; set; }

    public bool? Programozos { get; set; }

    public bool? Kifejtos { get; set; }

    public bool? Feleletvalasztos { get; set; }

    public string? Programnyelv { get; set; }

    public virtual ICollection<Allaskerdoiv> Allaskerdoivs { get; set; } = new List<Allaskerdoiv>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;

    public virtual ICollection<Kitoltottkerde> Kitoltottkerdes { get; set; } = new List<Kitoltottkerde>();

    public virtual ICollection<Teszteset> Tesztesets { get; set; } = new List<Teszteset>();

    public virtual ICollection<Valasz> Valaszs { get; set; } = new List<Valasz>();
}
