using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.DTOs.ItemDtos;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ItemRepo
{
    public class ItemRepository:IItemRepository
    {
        private readonly StockContext _context;
        ItemDetailsResult itemDetailsResult = new ItemDetailsResult();

        public ItemRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<ItemDetailsResult> GetItems(DTOPaging paging)
        {
            ItemDetailsResult itemDetailsResult=new ItemDetailsResult();
            itemDetailsResult.Total= await _context.Items.CountAsync(); 
             itemDetailsResult.ItemsDetails = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    ItemCode = item.ItemCode,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                })
                                .OrderBy(i => i.ItemId)  
                                .Skip((paging.PageNumber - 1) * paging.PageSize) 
                                .Take(paging.PageSize)  
                                .ToListAsync();

            return itemDetailsResult;
        }

        public async Task<ItemDetailsDto?> GetItemById(int id)
        {
            if (id <= 0) return null;
            var result = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                where item.ItemId == id
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    ItemCode = item.ItemCode,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                }).FirstOrDefaultAsync(); ;

            return result;
        }


        public async Task<ItemDetailsResult> GetItemsByCategoryId(int catId, DTOPaging paging)
        {
            // Initialize result object to store item details and total record count
            ItemDetailsResult itemDetailsResult = new ItemDetailsResult();

            // Fetch total item count for the specified category (catId)
            itemDetailsResult.Total = await _context.Items
                                                    .Where(item => item.CatFk == catId)
                                                    .CountAsync();

            // Fetch paginated item details for the specified category
            itemDetailsResult.ItemsDetails = await (
                                                    from item in _context.Items
                                                    join unit in _context.Units on item.UniteFk equals unit.UnitId
                                                    join category in _context.Categories on item.CatFk equals category.CatId
                                                    join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                                    where item.CatFk == catId
                                                    select new ItemDetailsDto
                                                    {
                                                        ItemId = item.ItemId,
                                                        ItemName = item.ItemName,
                                                        ItemCode = item.ItemCode,
                                                        UnitName = unit.UnitName,
                                                        CategoryName = category.CatNameEn,
                                                        CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()
                                                    })
                                                    .OrderBy(i => i.ItemId)  // Sort by ItemId or any desired field
                                                    .Skip((paging.PageNumber - 1) * paging.PageSize)  // Skip records for previous pages
                                                    .Take(paging.PageSize)  // Fetch the required page size
                                                    .ToListAsync();

            return itemDetailsResult;  // Return the result with paginated item details and total records
        }

        public async Task<ItemDetailsResult> GetItemsBySubWHId(int subId, DTOPaging paging)
        {
            itemDetailsResult.Total = await _context.Items.CountAsync();
            itemDetailsResult.ItemsDetails = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                join subItem in _context.SubItems on item.ItemId equals subItem.ItemFk
                                where subItem.SubFk == subId  // Filter by subId from SubItem table
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    ItemCode = item.ItemCode,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault() 
                                })
                                .OrderBy(i => i.ItemId)  
                                .Skip((paging.PageNumber - 1) * paging.PageSize) 
                                .Take(paging.PageSize) 
                                .ToListAsync(); 

            return itemDetailsResult;
        }

        public async Task<ItemDetailsResult> GetItemsByUnitId(int unitId, DTOPaging paging)
        {
            itemDetailsResult.Total = await _context.Items.CountAsync();
            itemDetailsResult.ItemsDetails = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                where item.UniteFk == unitId
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    ItemCode = item.ItemCode,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn, 
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                })
                               .OrderBy(i => i.ItemId)  
                               .Skip((paging.PageNumber - 1) * paging.PageSize) 
                               .Take(paging.PageSize)  
                               .ToListAsync(); 

            return itemDetailsResult;
        }

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
                                    ItemCode = item.ItemCode,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                }).ToListAsync(); 

            return result;
        }

    }
}
