using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopstask
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string? Title { get; set; }

    public string? Desc { get; set; }

    public string? Validation { get; set; }

    public double? Points { get; set; }

    public virtual Devop Devops { get; set; } = null!;

    public virtual ICollection<Devopstaskimplementation> Devopstaskimplementations { get; set; } = new List<Devopstaskimplementation>();
}
