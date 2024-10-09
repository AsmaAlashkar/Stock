using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class PermissionType
{
    public int PerId { get; set; }

    public string PerTypeValue { get; set; } = null!;

    public int PermissionFk { get; set; }

    public virtual Permission PermissionFkNavigation { get; set; } = null!;
}
