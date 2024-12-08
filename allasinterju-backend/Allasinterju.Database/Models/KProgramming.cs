using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class KProgramming
{
    public int Id { get; set; }

    public int Programmingid { get; set; }

    public int Kitoltottkerdoivid { get; set; }

    public string Programkod { get; set; } = null!;

    public virtual ICollection<KProgrammingtestcase> KProgrammingtestcases { get; set; } = new List<KProgrammingtestcase>();

    public virtual Kitoltottkerdoiv Kitoltottkerdoiv { get; set; } = null!;

    public virtual Programming Programming { get; set; } = null!;
}
