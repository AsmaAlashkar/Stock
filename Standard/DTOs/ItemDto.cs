using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = null!;
        public DateTime? ItemExperationdate { get; set; }
        public DateTime? ItemCreatedat { get; set; }
        public DateTime? ItemUpdatedat { get; set; }
        public bool? Delet { get; set; }
        public string CatNameEn { get; set; }
        public string? UnitName { get; set; }
        public string? SubName { get; set; }
    }
}
