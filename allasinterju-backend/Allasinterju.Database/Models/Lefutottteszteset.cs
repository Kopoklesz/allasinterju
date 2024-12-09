﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Lefutottteszteset
{
    public int Id { get; set; }

    public int Tesztesetid { get; set; }

    public int Kitoltottkerdesid { get; set; }

    public string? Kimenet { get; set; }

    public bool? Helyes { get; set; }

    public string? Token { get; set; }

    public string? Hibakimenet { get; set; }

    public double? Futasido { get; set; }

    public virtual Kitoltottkerde Kitoltottkerdes { get; set; } = null!;

    public virtual Teszteset Teszteset { get; set; } = null!;
}