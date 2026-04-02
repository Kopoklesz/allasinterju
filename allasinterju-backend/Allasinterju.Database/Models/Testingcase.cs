using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testingcase
{
    public int Id { get; set; }

    public int Testingid { get; set; }

    public string? Title { get; set; }

    public string? Expectedresult { get; set; }

    public string? Testdata { get; set; }

    public bool? Canbeautomated { get; set; }

    public int? Points { get; set; }

    public virtual Testing Testing { get; set; } = null!;

    public virtual ICollection<Testingcasestep> Testingcasesteps { get; set; } = new List<Testingcasestep>();
}
