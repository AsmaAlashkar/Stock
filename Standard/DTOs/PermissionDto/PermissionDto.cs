using Standard.DTOs.ItemDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.PermissionDto
{
    public class PermissionDto
    {
        public int PermId { get; set; }

        public int PermTypeFk { get; set; }
        public List<ItemDto> Items { get; set; }

        public DateTime? PermCreatedat { get; set; } = DateTime.Now;
    }
}
