using System;
using System.Collections.Generic;

namespace NulllogiconeCore.Models;

public partial class Löcher
{
    public Guid LochGuid { get; set; }

    public Guid? AnglerGuid { get; set; }

    public Guid NetzGuid { get; set; }

    public Guid KnotenGuid { get; set; }

    public Guid? BaumGuid { get; set; }

    public Guid? ZweigGuid { get; set; }

    public int Ilos { get; set; }

    public int Fit { get; set; }

    public virtual Angler? Angler { get; set; }
}

