using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Programming
{
    public int Id { get; set; }

    public int Kerdoivid { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Language { get; set; } = null!;

    public string? Codetemplate { get; set; }

    public virtual ICollection<KProgramming> KProgrammings { get; set; } = new List<KProgramming>();

    public virtual Kerdoiv Kerdoiv { get; set; } = null!;

    public virtual ICollection<Programmingtestcase> Programmingtestcases { get; set; } = new List<Programmingtestcase>();
}
