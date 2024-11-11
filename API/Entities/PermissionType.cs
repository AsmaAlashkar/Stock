using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class PermissionType
{
    public int PerId { get; set; }

    public string PerTypeValueAr { get; set; } = null!;

    public string PerTypeValueEn { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
