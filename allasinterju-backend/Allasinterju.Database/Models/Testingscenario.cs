using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testingscenario
{
    public int Id { get; set; }

    public int Testingid { get; set; }

    public string? Title { get; set; }

    public string? Desc { get; set; }

    public string? Prereq { get; set; }

    public string? Priority { get; set; }

    public virtual Testing Testing { get; set; } = null!;
}
