﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.DTOs
{
    public class CategoryDto
    {
        public int CatId { get; set; }

        public int? ParentCategoryId { get; set; }

        public string CatNameAr { get; set; } = null!;

        public string? CatNameEn { get; set; }

        public string? CatDesAr { get; set; }

        public string? CatDesEn { get; set; }

    }
}
