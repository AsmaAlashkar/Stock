using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.ItemDtos
{
    public class ItemsNamesDto
    {
        public int ItemId { get; set; }
        public string ItemNameEn { get; set; } = null!;
        public string ItemNameAr { get; set; } = null!;
        public string ItemCode { get; set; } = null!;

    }
}
