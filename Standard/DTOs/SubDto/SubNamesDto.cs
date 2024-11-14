using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.SubDto
{
    public class SubNamesDto
    {
        public int SubId { get; set; }
        public string SubNameEn { get; set; } = null!;

        public string SubNameAr { get; set; } = null!;
    }
}
