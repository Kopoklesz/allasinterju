<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kompetencium
{
    public int Id { get; set; }

    public string Tipus { get; set; } = null!;

<<<<<<< HEAD
    public string Leiras { get; set; } = null!;

    public virtual ICollection<Felhasznalokompetencium> Felhasznalokompetencia { get; set; } = new List<Felhasznalokompetencium>();
}
=======
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kompetencium
{
    public int Id { get; set; }

    public string Tipus { get; set; } = null!;

    public string Leiras { get; set; } = null!;

    public virtual ICollection<Felhasznalokompetencium> Felhasznalokompetencia { get; set; } = new List<Felhasznalokompetencium>();
}
>>>>>>> backend
=======
    public virtual ICollection<Allaskompetencium> Allaskompetencia { get; set; } = new List<Allaskompetencium>();

    public virtual ICollection<Felhasznalokompetencium> Felhasznalokompetencia { get; set; } = new List<Felhasznalokompetencium>();
}
>>>>>>> e1fd60460e0212373bbaaf76396fc787a1a232fc
