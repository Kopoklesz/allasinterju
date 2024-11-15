<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottkerde
{
    public int Id { get; set; }

    public int Kitoltottallasid { get; set; }

    public int Kerdesid { get; set; }

    public int? Kitolteskezdet { get; set; }

    public int? Elertpont { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual Kitoltottalla Kitoltottallas { get; set; } = null!;

    public virtual ICollection<Kitoltottvalasz> Kitoltottvalaszs { get; set; } = new List<Kitoltottvalasz>();
}
=======
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Kitoltottkerde
{
    public int Id { get; set; }

    public int Kitoltottallasid { get; set; }

    public int Kerdesid { get; set; }

    public int? Kitolteskezdet { get; set; }

    public int? Elertpont { get; set; }

    public virtual Kerde Kerdes { get; set; } = null!;

    public virtual Kitoltottalla Kitoltottallas { get; set; } = null!;

    public virtual ICollection<Kitoltottvalasz> Kitoltottvalaszs { get; set; } = new List<Kitoltottvalasz>();
}
>>>>>>> backend
