using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottvalasz
{
    public int Id { get; set; }

    public string? Szovegesvalasz { get; set; }

    public int? Elertpont { get; set; }

    public int Kitoltottkerdesid { get; set; }

    public int? Valaszid { get; set; }

    public virtual Kitoltottkerde Kitoltottkerdes { get; set; } = null!;

    public virtual Valasz? Valasz { get; set; }
}
