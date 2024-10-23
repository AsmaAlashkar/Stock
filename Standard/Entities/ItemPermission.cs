using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class ItemPermission
{
    public int ItemPerId { get; set; }

    public int PermFk { get; set; }

    public int ItemFk { get; set; }

    public double Quantity { get; set; }

    public virtual Item ItemFkNavigation { get; set; } = null!;

    public virtual Permission PermFkNavigation { get; set; } = null!;
}
