using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Unit
{
    public int UnitId { get; set; }

    public string UnitNameEn { get; set; } = null!;

    public string UnitNameAr { get; set; } = null!;

    public string? UnitDescEn { get; set; }

    public string? UnitDescAr { get; set; }

    public DateTime? UnitCreatedAt { get; set; }

    public DateTime? UnitUpdatedAt { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
