using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottkerdoiv
{
    public int Id { get; set; }

    public int Kor { get; set; }

    public int Kitoltottallasid { get; set; }

    public int Kerdoivid { get; set; }

    public DateTime? Kitolteskezdet { get; set; }
<<<<<<< HEAD

    public int? Osszpont { get; set; }

    public bool Befejezve { get; set; }

=======

    public bool Befejezve { get; set; }

    public double? Szazalek { get; set; }

>>>>>>> cbc6b116792ec3f21801ce54873d1174bafdb0fa
    public bool? Tovabbjut { get; set; }

    public bool? Miajanlas { get; set; }

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;

    public virtual Kitoltottalla Kitoltottallas { get; set; } = null!;

    public virtual ICollection<Kitoltottkerde> Kitoltottkerdes { get; set; } = new List<Kitoltottkerde>();
}
