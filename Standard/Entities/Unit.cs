using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public string? UnitDesc { get; set; }

    public DateTime? UnitCreatedAt { get; set; }

    public DateTime? UnitUpdatedAt { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
