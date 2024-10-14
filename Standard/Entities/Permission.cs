using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Permission
{
    public int PermId { get; set; }

    public int? PermTypeFk { get; set; }

    public DateTime? PermCreatedat { get; set; }

    public virtual ICollection<ItemPermission> ItemPermissions { get; set; } = new List<ItemPermission>();

    public virtual PermissionType? PermTypeFkNavigation { get; set; }
}
