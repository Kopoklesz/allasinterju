using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Alla
{
    public int Id { get; set; }

    public string Cim { get; set; } = null!;

    public string Munkakor { get; set; } = null!;

    public string? Munkarend { get; set; }

    public string Leiras { get; set; } = null!;

    public int Cegid { get; set; }

    public int Cegid { get; set; }

    public DateTime? Hatarido { get; set; }

    public string? Telephelyszoveg { get; set; }

    public virtual ICollection<Allaskapcsolattarto> Allaskapcsolattartos { get; set; } = new List<Allaskapcsolattarto>();

    public virtual ICollection<Allaskapcsolattarto> Allaskapcsolattartos { get; set; } = new List<Allaskapcsolattarto>();

    public virtual ICollection<Allaskompetencium> Allaskompetencia { get; set; } = new List<Allaskompetencium>();

    public virtual ICollection<Allasvizsgalo> Allasvizsgalos { get; set; } = new List<Allasvizsgalo>();

    public virtual Ceg Ceg { get; set; } = null!;

    public virtual ICollection<Kitoltottalla> Kitoltottallas { get; set; } = new List<Kitoltottalla>();
}
