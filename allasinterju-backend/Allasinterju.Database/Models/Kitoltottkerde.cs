﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottkerde
{
    public int Id { get; set; }

    public int Kitoltottkerdoivid { get; set; }

    public int Kerdesid { get; set; }

    public int? Kitolteskezdet { get; set; }

    public int? Elertpont { get; set; }

    public string? Szovegesvalasz { get; set; }

    public int? Valasztosid { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual Kitoltottkerdoiv Kitoltottkerdoiv { get; set; } = null!;

    public virtual ICollection<Kitoltottvalasz> Kitoltottvalaszs { get; set; } = new List<Kitoltottvalasz>();
}
