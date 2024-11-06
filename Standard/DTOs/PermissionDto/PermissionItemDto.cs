using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.PermissionDto
{
    public class PermissionItemDto
    {
        public int ItemId { get; set; }
        public string ItemCode { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string? CatNameEn { get; set; }
        public string? UnitName { get; set; }
        public double Quantity { get; set; }
    }
}
