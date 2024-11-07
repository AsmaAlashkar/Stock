using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class SubWearHouseDTO
    {
        public int SubId { get; set; }

        public int MainFk { get; set; }

        public int? ParentSubWearhouseId { get; set; }

        public string SubNameEn { get; set; } = null!;

        public string SubNameAr { get; set; } = null!;

        public string? SubDescriptionEn { get; set; }

        public string? SubDescriptionAr { get; set; }

        public string? SubAddressEn { get; set; }

        public string? SubAddressAr { get; set; }

        public DateTime? SubCreatedat { get; set; }

        public DateTime? SubUpdatedat { get; set; }

        public bool? Delet { get; set; }
    }
}
