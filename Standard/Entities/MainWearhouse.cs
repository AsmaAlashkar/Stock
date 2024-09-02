using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class MainWearhouse
{
    public int MainId { get; set; }

    public string MainName { get; set; } = null!;

    public string? MainDescription { get; set; }

    public string? MainAdderess { get; set; }

    public DateTime? MainCreatedat { get; set; }

    public DateTime? MainUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual ICollection<SubWearhouse> SubWearhouses { get; set; } = new List<SubWearhouse>();
}
