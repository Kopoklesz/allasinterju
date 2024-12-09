using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devop
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public string Tasktitle { get; set; } = null!;

    public string? Category { get; set; }

    public string? Difficulty { get; set; }

    public string Taskdescription { get; set; } = null!;

    public string? Platform { get; set; }

    public string? Systemrequirements { get; set; }

    public string? Resourcelimits { get; set; }

    public string? Accessrequirements { get; set; }

    public string? Architecturedesc { get; set; }

    public string? Infraconstraints { get; set; }

    public bool? Docrequired { get; set; }

    public string? Docformat { get; set; }

    public virtual ICollection<Devopscomponent> Devopscomponents { get; set; } = new List<Devopscomponent>();

    public virtual ICollection<Devopsdeliverable> Devopsdeliverables { get; set; } = new List<Devopsdeliverable>();

    public virtual ICollection<Devopsdocumentation> Devopsdocumentations { get; set; } = new List<Devopsdocumentation>();

    public virtual ICollection<Devopsevaluation> Devopsevaluations { get; set; } = new List<Devopsevaluation>();

    public virtual ICollection<Devopsprereq> Devopsprereqs { get; set; } = new List<Devopsprereq>();

    public virtual ICollection<Devopstask> Devopstasks { get; set; } = new List<Devopstask>();

    public virtual ICollection<KTobbi> KTobbis { get; set; } = new List<KTobbi>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;
}
