using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Lefutottteszteset
{
    public int Id { get; set; }

    public int Tesztesetid { get; set; }

    public int Kitoltottkerdesid { get; set; }

<<<<<<< HEAD
    public string? Kimenet { get; set; }

    public bool? Helyes { get; set; }

    public string? Token { get; set; }

    public string? Hibakimenet { get; set; }

    public double? Futasido { get; set; }
=======
    public int? Kimenet { get; set; }

    public int Helyes { get; set; }
>>>>>>> 2daedc2fac9c9c8eb3c1c10b32464ff7fa763403

    public virtual Kitoltottkerde Kitoltottkerdes { get; set; } = null!;

    public virtual Teszteset Teszteset { get; set; } = null!;
}
