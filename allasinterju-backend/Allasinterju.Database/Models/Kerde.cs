using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kerde
{
    public int Id { get; set; }

    public string Szoveg { get; set; } = null!;

    public TimeOnly? Kitoltesido { get; set; }

    public int? Maxpont { get; set; }

    public virtual ICollection<Allaskerdes> Allaskerdes { get; set; } = new List<Allaskerdes>();

    public virtual ICollection<Kitoltottkerdes> Kitoltottkerdes { get; set; } = new List<Kitoltottkerdes>();

    public virtual ICollection<Valasz> Valaszs { get; set; } = new List<Valasz>();
}
