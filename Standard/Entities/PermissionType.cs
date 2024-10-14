using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class PermissionType
{
    public int PerId { get; set; }

    public string PerTypeValue { get; set; } = null!;

    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
