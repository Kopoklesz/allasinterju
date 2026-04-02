using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Valasz
{

    public int Id { get; set; }

    public string Szoveg { get; set; } = null!;

     public int Kerdesid { get; set; }

    public bool? Helyes { get; set; }

    public int? Pontszam { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual ICollection<Kitoltottkerde> Kitoltottkerdes { get; set; } = new List<Kitoltottkerde>();

    public virtual ICollection<Kitoltottvalasz> Kitoltottvalaszs { get; set; } = new List<Kitoltottvalasz>();

}