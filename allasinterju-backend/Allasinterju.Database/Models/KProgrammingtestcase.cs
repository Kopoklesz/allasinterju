using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class KProgrammingtestcase
{
    public int Id { get; set; }

    public int KProgrammingid { get; set; }

    public int Programmingtestcaseid { get; set; }

    public string? Stdout { get; set; }

    public string? Stderr { get; set; }

    public double? Memoria { get; set; }

    public int? Futasido { get; set; }

    public bool Lefutott { get; set; }

    public bool? Nemfutle { get; set; }

    public bool? Helyes { get; set; }

    public virtual KProgramming KProgramming { get; set; } = null!;

    public virtual Programmingtestcase Programmingtestcase { get; set; } = null!;
}
