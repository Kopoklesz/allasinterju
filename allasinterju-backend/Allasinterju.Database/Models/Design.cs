using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Design
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public string Title { get; set; } = null!;

    public string? Category { get; set; }

    public string Description { get; set; } = null!;

    public string? Styleguide { get; set; }

    public string? Deliverables { get; set; }

    public virtual ICollection<Designevaluation> Designevaluations { get; set; } = new List<Designevaluation>();

    public virtual ICollection<Designreference> Designreferences { get; set; } = new List<Designreference>();

    public virtual ICollection<Designreq> Designreqs { get; set; } = new List<Designreq>();

    public virtual ICollection<KTobbi> KTobbis { get; set; } = new List<KTobbi>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;
}
