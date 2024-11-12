using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Allas
{
    public int Id { get; set; }

    public string Cim { get; set; } = null!;

    public string Munkakor { get; set; } = null!;

    public string? Munkarend { get; set; }

    public string Leiras { get; set; } = null!;

    public string? Rovidleiras { get; set; }

    public int Telephelyid { get; set; }

    public int Cegid { get; set; }

    public DateTime? Hatarido { get; set; }

    public TimeOnly? Kitoltesido { get; set; }

    public virtual ICollection<Allaskapcsolattarto> Allaskapcsolattartos { get; set; } = new List<Allaskapcsolattarto>();

    public virtual ICollection<Allaskerdes> Allaskerdes { get; set; } = new List<Allaskerdes>();

    public virtual ICollection<Allasvizsgalo> Allasvizsgalos { get; set; } = new List<Allasvizsgalo>();

    public virtual Ceg Ceg { get; set; } = null!;

    public virtual ICollection<Kitoltottallas> Kitoltottallas { get; set; } = new List<Kitoltottallas>();

    public virtual Cegtelephely Telephely { get; set; } = null!;
}
