using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kerdoiv
{
    public int Id { get; set; }

    public int Kor { get; set; }

    public int? Nev { get; set; }

    public int Allasid { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual ICollection<Allaskerdoiv> Allaskerdoivs { get; set; } = new List<Allaskerdoiv>();
}
