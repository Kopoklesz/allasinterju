using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Felhasznalo
{
    public int Id { get; set; }

    public string Vezeteknev { get; set; } = null!;

    public string Keresztnev { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Jelszo { get; set; } = null!;

    public long? Adoszam { get; set; }

    public string? Anyjaneve { get; set; }

    public DateTime? Szuldat { get; set; }

    public int? Szulirsz { get; set; }

    public string? Szulhely { get; set; }

    public bool Dolgozo { get; set; }

    public bool Allaskereso { get; set; }

    public int? Cegid { get; set; }

    public byte[]? Kep { get; set; }

    public string? Leetcode { get; set; }

    public virtual ICollection<Allaskapcsolattarto> Allaskapcsolattartos { get; set; } = new List<Allaskapcsolattarto>();

    public virtual ICollection<Allasvizsgalo> Allasvizsgalos { get; set; } = new List<Allasvizsgalo>();

    public virtual ICollection<Dokumentum> Dokumenta { get; set; } = new List<Dokumentum>();

    public virtual ICollection<Felhasznalokompetencium> Felhasznalokompetencia { get; set; } = new List<Felhasznalokompetencium>();

    public virtual ICollection<Kitoltottalla> Kitoltottallas { get; set; } = new List<Kitoltottalla>();
}
