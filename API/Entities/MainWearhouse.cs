using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class MainWearhouse
{
    public int MainId { get; set; }

    public string MainNameEn { get; set; } = null!;

    public string MainNameAr { get; set; } = null!;

    public string? MainDescriptionEn { get; set; }

    public string? MainDescriptionAr { get; set; }

    public string? MainAdderess { get; set; }

    public DateTime? MainCreatedat { get; set; }

    public DateTime? MainUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual ICollection<SubWearhouse> SubWearhouses { get; set; } = new List<SubWearhouse>();
}
