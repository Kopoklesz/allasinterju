<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Ceg
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Jelszo { get; set; }

    public string Cegnev { get; set; } = null!;

    public string Cegtipus { get; set; } = null!;

    public string? Leiras { get; set; }

    public int? Fotelephelyid { get; set; }

    public string? Levelezesicim { get; set; }

    public string? Kapcsolattarto { get; set; }

    public byte[]? Kep { get; set; }

    public string? Mobiltelefon { get; set; }

    public string? Telefon { get; set; }

    public string? Kapcsolattartonev { get; set; }

    public virtual ICollection<Alla> Allas { get; set; } = new List<Alla>();

    public virtual ICollection<Cegtelephely> Cegtelephelies { get; set; } = new List<Cegtelephely>();

    public virtual Cegtelephely? Fotelephely { get; set; }

    public virtual ICollection<Meghivokod> Meghivokods { get; set; } = new List<Meghivokod>();
}
=======
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Ceg
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Jelszo { get; set; }

    public string Cegnev { get; set; } = null!;

    public string Cegtipus { get; set; } = null!;

    public string? Leiras { get; set; }

    public int? Fotelephelyid { get; set; }

    public string? Levelezesicim { get; set; }

    public string? Kapcsolattarto { get; set; }

    public byte[]? Kep { get; set; }

    public string? Mobiltelefon { get; set; }

    public string? Telefon { get; set; }

    public string? Kapcsolattartonev { get; set; }

    public virtual ICollection<Alla> Allas { get; set; } = new List<Alla>();

    public virtual ICollection<Cegtelephely> Cegtelephelies { get; set; } = new List<Cegtelephely>();

    public virtual Cegtelephely? Fotelephely { get; set; }

    public virtual ICollection<Meghivokod> Meghivokods { get; set; } = new List<Meghivokod>();
}
>>>>>>> backend
