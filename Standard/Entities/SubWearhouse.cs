using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class SubWearhouse
{
    public int SubId { get; set; }

    public int MainFk { get; set; }

    public int? ParentSubWearhouseId { get; set; }

    public string SubName { get; set; } = null!;

    public string? SubDescription { get; set; }

    public string? SubAddress { get; set; }

    public DateTime? SubCreatedat { get; set; }

    public DateTime? SubUpdatedat { get; set; }

    public bool? Delet { get; set; }

    public virtual ICollection<SubWearhouse> InverseParentSubWearhouse { get; set; } = new List<SubWearhouse>();

    public virtual MainWearhouse MainFkNavigation { get; set; } = null!;

    public virtual SubWearhouse? ParentSubWearhouse { get; set; }

    public virtual ICollection<Permission> PermissionDestinationSubFkNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<Permission> PermissionSubFkNavigations { get; set; } = new List<Permission>();

    public virtual ICollection<SubItem> SubItems { get; set; } = new List<SubItem>();
}
