using System;
using System.Collections.Generic;

namespace NulllogiconeApi.Models;

public partial class CheckRinge
{
    public Guid RingGuid { get; set; }

    public string? Netz { get; set; }

    public string? Knoten { get; set; }

    public string? Baum { get; set; }

    public string? Zweig { get; set; }

    public Guid? BaumGuid { get; set; }

    public Guid? ZweigGuid { get; set; }
}
