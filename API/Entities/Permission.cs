using System;
using System.Collections.Generic;

namespace API.Entities;

public partial class Permission
{
    public int PermId { get; set; }

    public string PermCode { get; set; } = null!;

    public int? PermTypeFk { get; set; }

    public DateTime? PermCreatedat { get; set; }

    public int SubFk { get; set; }

    public int? DestinationSubFk { get; set; }

    public virtual SubWearhouse? DestinationSubFkNavigation { get; set; }

    public virtual ICollection<ItemPermission> ItemPermissions { get; set; } = new List<ItemPermission>();

    public virtual PermissionType? PermTypeFkNavigation { get; set; }

    public virtual SubWearhouse SubFkNavigation { get; set; } = null!;
}
