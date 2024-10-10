using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ItemRepo
{
    public class ItemRepository:IItemRepository
    {
        private readonly StockContext _context;
        public ItemRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItems()
        {
            return await _context.Items
                .AsNoTracking()
                .Include(i => i.CatFkNavigation)
                .Include(i => i.UniteFkNavigation)
                .Include(i => i.SubFkNavigation)
                .ToListAsync();

            //return await _context.Items.OrderBy(c => c.ItemId).ToListAsync();
        }

        public async Task<Item?> GetItemById(int id)
        {
            if (id <= 0) return null;
            return await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
        }

        public async Task<List<Item>> GetItemsByCategoryId(int catId)
        {
            return await _context.Items.Where(i => i.CatFk == catId).ToListAsync();
        }

        public async Task<List<Item>> GetItemsBySubWHId(int subId)
        {
            return await _context.Items.Where(s=>s.SubFk == subId).ToListAsync();
        }

        public async Task<List<Item>> GetItemsByUnitId(int unitId)
        {
            return await _context.Items.Where(u=>u.UniteFk == unitId).ToListAsync();
        }
        //public async Task<Item?> GetItemById(int id)
        //{

        //}

        public async Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync()
        {
            var result = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                }).ToListAsync(); 

            return result;
        }
    }
}
