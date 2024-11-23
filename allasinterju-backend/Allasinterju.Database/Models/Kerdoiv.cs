using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kerdoiv
{
    public int Id { get; set; }

    public int Kor { get; set; }

    public string? Nev { get; set; }

    public int Allasid { get; set; }

    public int? Maxpont { get; set; }

    public int? Kitoltesperc { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual ICollection<Allaskerdoiv> Allaskerdoivs { get; set; } = new List<Allaskerdoiv>();

    public virtual ICollection<Kerde> Kerdes { get; set; } = new List<Kerde>();

    public virtual ICollection<Kitoltottkerdoiv> Kitoltottkerdoivs { get; set; } = new List<Kitoltottkerdoiv>();
}
