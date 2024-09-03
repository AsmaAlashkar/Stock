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

        public string MainName { get; set; } = null!;

        public string? MainDescription { get; set; }

        public string? MainAdderess { get; set; }

        public DateTime? MainCreatedat { get; set; }

        public DateTime? MainUpdatedat { get; set; }

        public bool? Delet { get; set; }
    }
}
