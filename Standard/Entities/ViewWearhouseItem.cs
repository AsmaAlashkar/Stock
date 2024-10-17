using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class ViewWearhouseItem
{
    public int MainId { get; set; }

    public string MainName { get; set; } = null!;

    public string? MainDescription { get; set; }

    public string? MainAdderess { get; set; }

    public DateTime? MainCreatedat { get; set; }

    public DateTime? MainUpdatedat { get; set; }

    public int? SubId { get; set; }

    public string? SubName { get; set; }

    public string? SubAddress { get; set; }

    public DateTime? SubCreatedat { get; set; }

    public DateTime? SubUpdatedat { get; set; }

    public int? ItemId { get; set; }

    public string? ItemName { get; set; }

    public DateTime? ItemExperationdate { get; set; }

    public bool? Md { get; set; }

    public bool? Sd { get; set; }

    public bool? Id { get; set; }

    public int? Expr1 { get; set; }

    public int? SubFk { get; set; }
}
