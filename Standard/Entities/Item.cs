﻿using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemCode { get; set; } = null!;

    public string ItemNameEn { get; set; } = null!;

    public string ItemNameAr { get; set; } = null!;

    public int CatFk { get; set; }
     
    public int? UniteFk { get; set; }

    public DateTime? ItemExperationdate { get; set; }

    public DateTime? ItemCreatedat { get; set; }

    public DateTime? ItemUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual Category CatFkNavigation { get; set; } = null!;

    public virtual ICollection<ItemPermission> ItemPermissions { get; set; } = new List<ItemPermission>();

    public virtual ICollection<ItemSupplier> ItemSuppliers { get; set; } = new List<ItemSupplier>();

    public virtual ICollection<Quantity> Quantities { get; set; } = new List<Quantity>();

    public virtual ICollection<SubItem> SubItems { get; set; } = new List<SubItem>();

    public virtual Unit? UniteFkNavigation { get; set; }
}
