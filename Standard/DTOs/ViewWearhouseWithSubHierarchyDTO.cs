using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class ViewWearhouseWithSubHierarchyDTO
    {
        public int MainId { get; set; }

        public string MainNameEn { get; set; } = null!;

        public string MainNameAr { get; set; } = null!;

        public string? MainDescriptionEn { get; set; }

        public string? MainDescriptionAr { get; set; }

        public string? MainAdderess { get; set; }

        public DateTime? MainCreatedat { get; set; }

        public DateTime? MainUpdatedat { get; set; }

        public int? SubId { get; set; }

        public string? SubNameEn { get; set; }

        public string? SubNameAr { get; set; }

        public string? SubAddressEn { get; set; }

        public string? SubAddressAr { get; set; }

        public DateTime? SubCreatedat { get; set; }

        public DateTime? SubUpdatedat { get; set; }

        public string? SubDescriptionEn { get; set; }

        public string? SubDescriptionAr { get; set; }

        public int? ParentSubWearhouseId { get; set; }

        public int? Level { get; set; }

        public int? ItemId { get; set; }

        public string? ItemNameEn { get; set; }

        public string? ItemNameAr { get; set; }

        public DateTime? ItemExperationdate { get; set; }

        public bool? Md { get; set; }

        public bool? Sd { get; set; }

        public bool? Id { get; set; }
        // Recursive structure for child sub-warehouses
        public List<ViewWearhouseWithSubHierarchyDTO> Children { get; set; }
    }
}
