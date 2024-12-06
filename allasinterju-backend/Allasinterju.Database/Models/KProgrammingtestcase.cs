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

    public double? Futasido { get; set; }

    public string Token { get; set; } = null!;

    public bool Lefutott { get; set; }

    public virtual KProgramming KProgramming { get; set; } = null!;

    public virtual Programmingtestcase Programmingtestcase { get; set; } = null!;
}
