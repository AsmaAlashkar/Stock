﻿using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryRepo
{
    public interface ICategoryRepository
    {
        Task<List<CategoriesHirarichy>> GetCategories();
        Task<CategoriesHirarichy?> GetCategoryById(int id);
        Task<List<CategoryDto>> GetSubCategoriesByCategoryFk(int categoryFk);


    }
}
