using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Devopsdeliverable
{
    public int Id { get; set; }

    public int Devopsid { get; set; }

    public string? Title { get; set; }

    public string? Desc { get; set; }

    public string? Acceptance { get; set; }

    public string? Format { get; set; }

    public virtual Devop Devops { get; set; } = null!;
}
