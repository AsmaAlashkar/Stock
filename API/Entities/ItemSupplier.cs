using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class ItemSupplier
{
    public int ItemSuppliersId { get; set; }

    public int? ItemsFk { get; set; }

    public int? SuppliersFk { get; set; }

    public virtual Item? ItemsFkNavigation { get; set; }

    public virtual Supplier? SuppliersFkNavigation { get; set; }
}
