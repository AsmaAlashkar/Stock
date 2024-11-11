using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class PermissionTypeDto
    {
        public int PerId { get; set; }

        public string PerTypeValueAr { get; set; } = null!;

        public string PerTypeValueEn { get; set; } = null!;
    }
}
