using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.PermissionDto
{
    public class PermissionItemDto
    {
        public int ItemId { get; set; }
        public int SourceSubId { get; set; } 
        public int DestinationSubId { get; set; } 
        public int Quantity { get; set; }
    }
}
