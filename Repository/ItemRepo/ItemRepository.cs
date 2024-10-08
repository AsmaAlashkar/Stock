using Microsoft.EntityFrameworkCore;
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
            return await _context.Items.OrderBy(c => c.ItemId).ToListAsync();
        }

        //public async Task<Item?> GetItemById(int id)
        //{
            
        //}
    }
}
