using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CategoryRepo
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly StockContext _context;

        public CategoryRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriesHirarichy>> GetCategories()
        {
            return await _context.CategoriesHirarichies.OrderBy(c=>c.CatId).ToListAsync();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c=>c.CatId == id);
        }

        public Task<List<CategoryDto>> GetSubCategoriesByCategoryFk(int categoryFk)
        {
            throw new NotImplementedException();
        }
    }
}
