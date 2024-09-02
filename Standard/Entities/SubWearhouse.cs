using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class SubWearhouse
{
    public int SubId { get; set; }

    public int MainFk { get; set; }

    public string? SubDescription { get; set; }

    public string? SubAddress { get; set; }

    public DateTime? SubCreatedat { get; set; }

    public DateTime? SubUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual MainWearhouse MainFkNavigation { get; set; } = null!;
}
