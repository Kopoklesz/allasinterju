﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottalla
{
    public int Id { get; set; }

    public int Allaskeresoid { get; set; }

    public int Allasid { get; set; }

    public int Kitolteskezdet { get; set; }

    public virtual Alla Allas { get; set; } = null!;

    public virtual Felhasznalo Allaskereso { get; set; } = null!;

    public virtual ICollection<Kitoltottkerde> Kitoltottkerdes { get; set; } = new List<Kitoltottkerde>();
}
