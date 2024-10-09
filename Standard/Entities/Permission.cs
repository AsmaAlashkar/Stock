using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Permission
{
    public int PermId { get; set; }

    public double Quantity { get; set; }

    public DateTime? PermCreatedat { get; set; }

    public virtual ICollection<ItemPermission> ItemPermissions { get; set; } = new List<ItemPermission>();

    public virtual ICollection<PermissionType> PermissionTypes { get; set; } = new List<PermissionType>();
}
