using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class SubItem
{
    public int Id { get; set; }

    public int ItemFk { get; set; }

    public int SubFk { get; set; }

    public double? Quantity { get; set; }

    public virtual Item ItemFkNavigation { get; set; } = null!;

    public virtual SubWearhouse SubFkNavigation { get; set; } = null!;
}
