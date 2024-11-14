using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Category
{
    public int CatId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string CatNameAr { get; set; } = null!;

    public string CatNameEn { get; set; } = null!;

    public string? CatDesAr { get; set; }

    public string? CatDesEn { get; set; }

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual Category? ParentCategory { get; set; }
}
