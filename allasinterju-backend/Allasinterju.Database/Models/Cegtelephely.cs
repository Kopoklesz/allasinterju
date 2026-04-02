using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Cegtelephely
{
    public int Id { get; set; }

    public string? Irsz { get; set; }

    public string? Telepules { get; set; }

    public string? Utcahazszam { get; set; }

    public string? Cimszoveg { get; set; }

    public int? Cegid { get; set; }
}