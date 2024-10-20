using System;
using System.Collections.Generic;

namespace Standard.Entities;

public partial class ItemSubWearhouse
{
    public int IswId { get; set; }

    public int? SubFk { get; set; }

    public int? ItemsFk { get; set; }

    public virtual Item? ItemsFkNavigation { get; set; }

    public virtual SubWearhouse? SubFkNavigation { get; set; }
}
