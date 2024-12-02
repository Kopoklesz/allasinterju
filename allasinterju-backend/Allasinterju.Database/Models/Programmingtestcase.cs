using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Programmingtestcase
{
    public int Id { get; set; }

    public int Programmingid { get; set; }

    public string? Input { get; set; }

    public string? Output { get; set; }

    public virtual Programming Programming { get; set; } = null!;
}
