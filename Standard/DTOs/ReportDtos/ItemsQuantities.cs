using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.ReportDtos
{
    public class ItemsQuantities
    {
        public string ItemNameEn { get; set; } = null!;
        public string ItemNameAr { get; set; } = null!;
        public double? CurrentQuantity { get; set; }

    }
}
