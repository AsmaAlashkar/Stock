using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs.ItemDtos
{
    public class ItemDetailsDto
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; } = null!;
        public string UnitName { get; set; }
        public string CategoryName { get; set; }
        public int CurrentQuantity { get; set; }
    }
}
