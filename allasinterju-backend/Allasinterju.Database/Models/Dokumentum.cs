using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Dokumentum
{
    public int Id { get; set; }

    public string? Leiras { get; set; }

    public string Fajlnev { get; set; } = null!;

    public byte[] Fajl { get; set; } = null!;

    public int Felhasznaloid { get; set; }

    public virtual Felhasznalo Felhasznalo { get; set; } = null!;
}
