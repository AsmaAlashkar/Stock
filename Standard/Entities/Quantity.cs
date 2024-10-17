using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Quantity
{
    public int QuantityId { get; set; }

    public int ItemFk { get; set; }

    public double? CurrentQuantity { get; set; }

    public DateTime? QuantityUpdatedat { get; set; }

    public DateTime? QuantityCreatedat { get; set; }

    public virtual Item ItemFkNavigation { get; set; } = null!;
}
