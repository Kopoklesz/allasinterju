using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopsdocumentation
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string? Title { get; set; }

    public string? Templatecontent { get; set; }

    public bool? Requiredtemplate { get; set; }

    public virtual Devop Devops { get; set; } = null!;
}
