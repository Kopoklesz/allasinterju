using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Lefutottteszteset
{
    public int Id { get; set; }

    public int Tesztesetid { get; set; }

    public int Kitoltottkerdesid { get; set; }

    public int? Kimenet { get; set; }

    public int Helyes { get; set; }

    public virtual Kitoltottkerde Kitoltottkerdes { get; set; } = null!;

    public virtual Teszteset Teszteset { get; set; } = null!;
}
