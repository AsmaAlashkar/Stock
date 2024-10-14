using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class ItemDetailsResult
    {
        public List<ItemDetailsDto> ItemsDetails { get; set; } = null!;
        public int Total { get; set; }
    }
}
