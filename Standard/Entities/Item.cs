﻿using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public int CatFk { get; set; }

    public int? SubFk { get; set; }

    public string ItemName { get; set; } = null!;

    public string ItemUnit { get; set; } = null!;

    public DateTime ItemExperationdate { get; set; }

    public DateTime? ItemCreatedat { get; set; }

    public DateTime? ItemUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual Category CatFkNavigation { get; set; } = null!;

    public virtual ICollection<ItemSupplier> ItemSuppliers { get; set; } = new List<ItemSupplier>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual SubWearhouse? SubFkNavigation { get; set; }
}