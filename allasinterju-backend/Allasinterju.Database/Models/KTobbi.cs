using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class KTobbi
{
    public int Id { get; set; }

    public int Kitoltottkerdoivid { get; set; }

    public int? Algorithmid { get; set; }

    public int? Designid { get; set; }

    public int? Testingid { get; set; }

    public int? Devopsid { get; set; }

    public string? Szovegesvalasz { get; set; }

    public virtual Algorithm? Algorithm { get; set; }

    public virtual Design? Design { get; set; }

    public virtual Devop? Devops { get; set; }

    public virtual Kitoltottkerdoiv Kitoltottkerdoiv { get; set; } = null!;

    public virtual Testing? Testing { get; set; }
}
