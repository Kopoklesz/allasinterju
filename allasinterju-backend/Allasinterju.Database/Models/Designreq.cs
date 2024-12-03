using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Designreq
{
    public int Id { get; set; }

    public int Designid { get; set; }

    public string? Category { get; set; }

    public string? Description { get; set; }

    public virtual Design Design { get; set; } = null!;
}
