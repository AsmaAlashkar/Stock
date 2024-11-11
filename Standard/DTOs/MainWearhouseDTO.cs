using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class MainWearhouseDTO
    {
        public int MainId { get; set; }

        public string MainNameEn { get; set; } = null!;

        public string MainNameAr { get; set; } = null!;

        public string? MainDescriptionEn { get; set; }

        public string? MainDescriptionAr { get; set; }

        public string? MainAdderess { get; set; }

        public DateTime? MainCreatedat { get; set; }

        public DateTime? MainUpdatedat { get; set; }

        public bool? Delet { get; set; }
    }
}
