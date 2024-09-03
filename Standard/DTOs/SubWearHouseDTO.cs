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

        public string SubName { get; set; } = null!;

        public string? SubDescription { get; set; }

        public string? SubAddress { get; set; }

        public DateTime? SubCreatedat { get; set; }

        public DateTime? SubUpdatedat { get; set; }

        public bool? Delet { get; set; }
    }
}
