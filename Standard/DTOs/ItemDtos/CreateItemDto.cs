using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.ItemDtos
{
    public class CreateItemDto
    {
        public string ItemCode { get; set; } = null!;

        public string ItemNameEn { get; set; } = null!;

        public string ItemNameAr { get; set; } = null!;

        public int CatFk { get; set; }

        public int? UniteFk { get; set; }

        public DateTime? ItemExperationdate { get; set; }

        public DateTime? ItemCreatedat { get; set; }

        public DateTime? ItemUpdatedat { get; set; }

    }
}
