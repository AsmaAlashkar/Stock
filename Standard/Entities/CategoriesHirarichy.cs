using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class CategoriesHirarichy
{
    public int? CatId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? CatNameAr { get; set; }

    public string? CatNameEn { get; set; }

    public string? CatDesAr { get; set; }

    public string? CatDesEn { get; set; }

    public int? Level { get; set; }
}
