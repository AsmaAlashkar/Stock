using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class Permission
{
    public int PermId { get; set; }

    public int? PermTypeFk { get; set; }

    public DateTime? PermCreatedat { get; set; }

    public virtual PermissionType? PermTypeFkNavigation { get; set; }

    public virtual ICollection<SubItemPermission> SubItemPermissions { get; set; } = new List<SubItemPermission>();
}
