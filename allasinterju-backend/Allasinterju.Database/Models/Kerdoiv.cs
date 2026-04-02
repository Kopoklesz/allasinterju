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

    public bool Programming { get; set; }

    public bool Design { get; set; }

    public bool Algorithm { get; set; }

    public bool Testing { get; set; }

    public bool Devops { get; set; }

    public virtual ICollection<Algorithm> Algorithms { get; set; } = new List<Algorithm>();

    public virtual Alla Allas { get; set; } = null!;

    public virtual ICollection<Allaskerdoiv> Allaskerdoivs { get; set; } = new List<Allaskerdoiv>();

    public virtual ICollection<Design> Designs { get; set; } = new List<Design>();

    public virtual ICollection<Devop> DevopsNavigation { get; set; } = new List<Devop>();

    public virtual ICollection<Kerde> Kerdes { get; set; } = new List<Kerde>();

    public virtual ICollection<Kitoltottkerdoiv> Kitoltottkerdoivs { get; set; } = new List<Kitoltottkerdoiv>();

    public virtual ICollection<Programming> Programmings { get; set; } = new List<Programming>();

    public virtual ICollection<Testing> Testings { get; set; } = new List<Testing>();
}