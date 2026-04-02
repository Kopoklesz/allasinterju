using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Testingcasestep
{
    public int Id { get; set; }

    public int Testingcaseid { get; set; }

    public string Teststep { get; set; } = null!;

    public virtual Testingcase Testingcase { get; set; } = null!;
}
