using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Stock
{
    public int StockId { get; set; }

    public int ItemFk { get; set; }

    public double? CurrentStockLevel { get; set; }

    public DateTime? StockUpdatedat { get; set; }

    public DateTime? StockCreatedat { get; set; }

    public virtual Item ItemFkNavigation { get; set; } = null!;
}
