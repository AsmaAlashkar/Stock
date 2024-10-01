using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class CategoryDto
    {
        public int? CatId { get; set; }

        public string? CatNameAr { get; set; }

        public int? ParentCategoryId { get; set; }

        public int? Level { get; set; }
    }
}
