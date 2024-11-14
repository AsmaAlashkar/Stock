using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.UnitDtos
{
    public class DisplayUnitNameDto
    {
        public int UnitId { get; set; }
        public string UnitNameEn { get; set; } = null!;

        public string UnitNameAr { get; set; } = null!;
    }
}
