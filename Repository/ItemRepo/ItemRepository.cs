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
            return await _context.Items.OrderBy(c => c.ItemId).ToListAsync();
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
                                    CategoryName = category.CatNameEn,  // Use the appropriate language field
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  // Safely handle nullable types
                                }).ToListAsync();  // Get the list of all items with their details

            return result;
        }
    }
}
