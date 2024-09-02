using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string SuppliersName { get; set; } = null!;

    public string? ContactPeraon { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? SuppliarAddreaa { get; set; }

    public bool? Delet { get; set; }

    public DateTime? SupCreatedat { get; set; }

    public DateTime? SupUpdatedat { get; set; }

    public virtual ICollection<ItemSupplier> ItemSuppliers { get; set; } = new List<ItemSupplier>();
}
