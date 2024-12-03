using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopstaskimplementation
{
    public int Id { get; set; }

    public int Devopstaskid { get; set; }

    public string Implementation { get; set; } = null!;

    public virtual Devopstask Devopstask { get; set; } = null!;
}
