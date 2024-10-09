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
            var items = await _context.Items
            .Include(i => i.CatFkNavigation) 
            .Include(i => i.UniteFkNavigation) 
            .Include(i => i.SubFkNavigation) 
            .Include(i => i.ItemSuppliers) 
                .ThenInclude(i => i.SuppliersFkNavigation) 
            .ToListAsync();

            return items;
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
    }
}
