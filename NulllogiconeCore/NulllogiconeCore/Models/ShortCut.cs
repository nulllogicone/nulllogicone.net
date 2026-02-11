using System;
using System.Collections.Generic;

namespace NulllogiconeCore.Models;

public partial class ShortCut
{
    public Guid ShortCutsGuid { get; set; }

    public Guid StammGuid { get; set; }

    public string ShortCut1 { get; set; } = null!;

    public bool Auto { get; set; }

    public virtual Stamm Stamm { get; set; } = null!;

    public virtual ICollection<StringEntity> Strings { get; set; } = new List<StringEntity>();
}

