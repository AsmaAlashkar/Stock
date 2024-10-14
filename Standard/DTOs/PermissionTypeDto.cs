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

        public string PerTypeValue { get; set; } = null!;
    }
}
