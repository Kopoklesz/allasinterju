<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Meghivokod
{
    public int Id { get; set; }

    public string Kod { get; set; } = null!;

    public DateTime? Ervenyesseg { get; set; }

    public int Cegid { get; set; }

    public virtual Ceg Ceg { get; set; } = null!;
}
=======
﻿using System;
using System.Collections.Generic;

namespace Allasinterju.Database.Models;

public partial class Meghivokod
{
    public int Id { get; set; }

    public string Kod { get; set; } = null!;

    public DateTime? Ervenyesseg { get; set; }

    public int Cegid { get; set; }

    public virtual Ceg Ceg { get; set; } = null!;
}
>>>>>>> backend
