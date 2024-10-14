using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.ItemDtos
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public int CatFk { get; set; }

        public int? UniteFk { get; set; }

        public int? SubFk { get; set; }

        public DateTime? ItemExperationdate { get; set; }

        public DateTime? ItemCreatedat { get; set; }

        public DateTime? ItemUpdatedat { get; set; }
        public int Quantity { get; set; }
    }
}
