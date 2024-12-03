using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottkerdoiv
{
    public int Id { get; set; }

    public int Kitoltottallasid { get; set; }

    public int Kerdoivid { get; set; }

    public DateTime? Kitolteskezdet { get; set; }

    public bool Befejezve { get; set; }

    public double? Szazalek { get; set; }

    public int? Miszazalek { get; set; }

    public bool? Tovabbjut { get; set; }

    public bool? Miajanlas { get; set; }

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;

    public virtual Kitoltottalla Kitoltottallas { get; set; } = null!;

    public virtual ICollection<Kitoltottkerde> Kitoltottkerdes { get; set; } = new List<Kitoltottkerde>();
}
