using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Ceg
{
    public int Id { get; set; }

    public string Cegnev { get; set; } = null!;

    public string Cegtipus { get; set; } = null!;

    public string? Leiras { get; set; }

    public int Fotelephelyid { get; set; }

    public string? Levelezesicim { get; set; }

    public string? Kapcsolattarto { get; set; }

    public int Felhasznaloid { get; set; }

    public byte[]? Kep { get; set; }

    public int? Mobiltelefon { get; set; }

    public int? Telefon { get; set; }

    public string? Kapcsolattartonev { get; set; }

    public virtual ICollection<Allas> Allas { get; set; } = new List<Allas>();

    public virtual ICollection<Cegtelephely> Cegtelephelies { get; set; } = new List<Cegtelephely>();

    public virtual ICollection<Felhasznalo> Felhasznalos { get; set; } = new List<Felhasznalo>();

    public virtual Cegtelephely Fotelephely { get; set; } = null!;

    public virtual ICollection<Meghivokod> Meghivokods { get; set; } = new List<Meghivokod>();
}
