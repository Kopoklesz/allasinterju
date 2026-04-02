using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Algorithm
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public string Title { get; set; } = null!;

    public string? Category { get; set; }

    public string? Difficulty { get; set; }

    public int? Memory { get; set; }

    public string Problemdesc { get; set; } = null!;

    public string? Inputformat { get; set; }

    public string? Outputformat { get; set; }

    public string? Timecomplexity { get; set; }

    public string? Spacecomplexity { get; set; }

    public string? Samplesolution { get; set; }

    public virtual ICollection<Algorithmconstraint> Algorithmconstraints { get; set; } = new List<Algorithmconstraint>();

    public virtual ICollection<Algorithmexample> Algorithmexamples { get; set; } = new List<Algorithmexample>();

    public virtual ICollection<Algorithmhint> Algorithmhints { get; set; } = new List<Algorithmhint>();

    public virtual ICollection<Algortihmtestcase> Algortihmtestcases { get; set; } = new List<Algortihmtestcase>();

    public virtual ICollection<KTobbi> KTobbis { get; set; } = new List<KTobbi>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;
}
