using AutoMapper;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Mapping.CategoryDtoProf
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoriesHirarichy, CategoryDto>().ReverseMap();
            CreateMap<CategoryDto, CategoriesHirarichy>().ReverseMap();

        }
    }
}
