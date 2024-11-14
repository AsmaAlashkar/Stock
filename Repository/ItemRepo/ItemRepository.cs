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
            ItemDetailsResult itemDetailsResult = new ItemDetailsResult();

            itemDetailsResult.Total = await _context.Items.CountAsync();

            itemDetailsResult.ItemsDetails = await _context.Items
                                                    .AsNoTracking() 
                                                    .Include(item => item.UniteFkNavigation) 
                                                    .Include(item => item.CatFkNavigation)
                                                    .Include(item => item.Quantities)        
                                                    .Select(item => new ItemDetailsDto
                                                    {
                                                        ItemId = item.ItemId,
                                                        ItemNameEn = item.ItemNameEn,
                                                        ItemNameAr = item.ItemNameAr,
                                                        ItemCode = item.ItemCode,
                                                        UnitNameEn = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameEn : "N/A", 
                                                        UnitNameAr = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameAr : "N/A", 
                                                        CatNameEn = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameEn : "N/A",       
                                                        CatNameAr = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameAr : "N/A",       
                                                        CurrentQuantity = item.Quantities
                                                            .Where(q => q.ItemFk == item.ItemId)  
                                                            .Sum(q => q.CurrentQuantity.HasValue ? (int)q.CurrentQuantity.Value : 0)
                                                    })
                                                    .OrderBy(i => i.ItemId) 
                                                    .Skip((paging.PageNumber - 1) * paging.PageSize)  
                                                    .Take(paging.PageSize)  
                                                    .ToListAsync();

            return itemDetailsResult; 
        }

        public async Task<List<ItemsNamesDto>> GetItemsNames()
        {
            var items = await _context.Items
                             .Select(item => new ItemsNamesDto
                             {
                                 ItemId = item.ItemId,
                                 ItemNameEn = item.ItemNameEn,
                                 ItemNameAr = item.ItemNameAr,
                                ItemCode = item.ItemCode
                             })
                             .ToListAsync();

            return items;
        }
        public async Task<List<ItemsNamesDto>> GetItemsNamesBySubId(int subId)
        {
            var items = await _context.Items
                .Where(item => item.SubItems.Any(sub => sub.SubFk == subId))
                             .Select(item => new ItemsNamesDto
                             {
                                 ItemId = item.ItemId,
                                 ItemNameEn = item.ItemNameEn,
                                 ItemNameAr = item.ItemNameAr,
                                 ItemCode = item.ItemCode
                             })
                             .ToListAsync();

            return items;
        }
        public async Task<ItemDetailsDto?> GetItemById(int id)
        {
            if (id <= 0) return null;
            var item = await _context.Items
                .Include(i => i.UniteFkNavigation)
                .Include(i => i.CatFkNavigation)  
                .Include(i => i.Quantities)      
                .FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null) return null;

            var itemDetailsDto = new ItemDetailsDto
            {
                ItemId = item.ItemId,
                ItemNameEn = item.ItemNameEn,
                ItemNameAr = item.ItemNameAr,
                ItemCode = item.ItemCode,
                UnitNameEn = item.UniteFkNavigation?.UnitNameEn,
                UnitNameAr = item.UniteFkNavigation?.UnitNameAr,
                CatNameEn = item.CatFkNavigation?.CatNameEn,
                CatNameAr = item.CatFkNavigation?.CatNameAr,
                CurrentQuantity = item.Quantities.FirstOrDefault()?.CurrentQuantity ?? 0
            };

            return itemDetailsDto;
        }

        public async Task<ItemDetailsDto?> GetItemDetailsBySubAsync(int itemId, int subId)
        {
            var itemExists = await _context.Items
                .AsNoTracking()
                .AnyAsync(i => i.ItemId == itemId);

            if (!itemExists)
            {
                return null; 
            }

            var subExists = await _context.SubWearhouses
                .AsNoTracking()
                .AnyAsync(sw => sw.SubId == subId);

            if (!subExists)
            {
                return null; 
            }
            var itemDto = await _context.Items
                .AsNoTracking()
                .Include(i => i.UniteFkNavigation)   
                .Include(i => i.CatFkNavigation)     
                .Include(i => i.SubItems)             
                .Where(i => i.ItemId == itemId)      
                .Select(i => new ItemDetailsDto
                {
                    ItemId = i.ItemId,
                    ItemNameEn = i.ItemNameEn,
                    ItemNameAr = i.ItemNameAr,
                    ItemCode = i.ItemCode,
                    UnitNameEn = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameEn : "N/A",  
                    UnitNameAr = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameAr : "N/A", 
                    CatNameEn = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameEn : "N/A",     
                    CatNameAr = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameAr : "N/A",
                    CurrentQuantity = i.SubItems
                                      .Where(si => si.SubFk == subId)
                                      .Sum(si => si.Quantity.HasValue ? (int)si.Quantity.Value : 0)  
                })
                .FirstOrDefaultAsync();

            return itemDto;
        }
        public async Task<ItemDetailsResult> GetItemsByCategoryId(int catId, DTOPaging paging)
        {
            ItemDetailsResult itemDetailsResult = new ItemDetailsResult();

            itemDetailsResult.Total = await _context.Items
                                                    .Where(item => item.CatFk == catId)
                                                    .CountAsync();

            itemDetailsResult.ItemsDetails = await _context.Items
                                                    .AsNoTracking() 
                                                    .Include(item => item.UniteFkNavigation)  
                                                    .Include(item => item.CatFkNavigation) 
                                                    .Include(item => item.Quantities)       
                                                    .Where(item => item.CatFk == catId)    
                                                    .Select(item => new ItemDetailsDto
                                                    {
                                                        ItemId = item.ItemId,
                                                        ItemNameEn = item.ItemNameEn,
                                                        ItemNameAr = item.ItemNameAr,
                                                        ItemCode = item.ItemCode,
                                                        UnitNameEn = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameEn : "N/A",
                                                        UnitNameAr = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameAr : "N/A",  
                                                        CatNameEn = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameEn : "N/A",      
                                                        CatNameAr = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameAr : "N/A",      
                                                        CurrentQuantity = item.Quantities
                                                            .Where(q => q.ItemFk == item.ItemId)  
                                                            .Sum(q => q.CurrentQuantity.HasValue ? q.CurrentQuantity.Value : 0) 
                                                    })
                                                    .OrderBy(i => i.ItemId)  
                                                    .Skip((paging.PageNumber - 1) * paging.PageSize)  
                                                    .Take(paging.PageSize)  
                                                    .ToListAsync();

            return itemDetailsResult;  
        }

        public async Task<ItemDetailsResult> GetItemsBySubWHId(int subId, DTOPaging paging)
        {
            var itemDetailsResult = new ItemDetailsResult();
            itemDetailsResult.Total = await _context.Items
                                                 .Where(item => item.SubItems.Any(subItem => subItem.SubFk == subId))
                                                 .CountAsync();

            itemDetailsResult.ItemsDetails = await _context.Items
                        .AsNoTracking()  
                        .Include(i => i.UniteFkNavigation)  
                        .Include(i => i.CatFkNavigation)   
                        .Include(i => i.Quantities)        
                        .Include(i => i.SubItems)         
                        .Where(i => i.SubItems.Any(subItem => subItem.SubFk == subId))
                        .Select(i => new ItemDetailsDto
                        {
                            ItemId = i.ItemId,
                            ItemNameEn = i.ItemNameEn,
                            ItemNameAr = i.ItemNameAr,
                            ItemCode = i.ItemCode,
                            UnitNameEn = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameEn : "N/A", 
                            UnitNameAr = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameAr : "N/A",  
                            CatNameEn = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameEn : "N/A",      
                            CatNameAr = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameAr : "N/A",      
                            CurrentQuantity = i.Quantities
                                .Where(q => q.ItemFk == i.ItemId) 
                                .Sum(q => q.CurrentQuantity.HasValue ? (int)q.CurrentQuantity.Value : 0)  
                        })
                                 .OrderBy(i => i.ItemId) 
                                 .Skip((paging.PageNumber - 1) * paging.PageSize) 
                                 .Take(paging.PageSize)  
                                 .ToListAsync();

            return itemDetailsResult;
        }

        public async Task<ItemDetailsResult> GetItemsByUnitId(int unitId, DTOPaging paging)
        {
            var itemDetailsResult = new ItemDetailsResult();

            itemDetailsResult.Total = await _context.Items
                .Where(item => item.UniteFk == unitId)
                .CountAsync();

            itemDetailsResult.ItemsDetails = await _context.Items
               .AsNoTracking()
               .Include(i => i.UniteFkNavigation) 
               .Include(i => i.CatFkNavigation)   
               .Include(i => i.Quantities)        
               .Where(i => i.UniteFk == unitId)   
               .Select(i => new ItemDetailsDto
               {
                   ItemId = i.ItemId,
                   ItemNameEn = i.ItemNameEn,
                   ItemNameAr = i.ItemNameAr,
                   ItemCode = i.ItemCode,
                   UnitNameEn = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameEn : "N/A", 
                   UnitNameAr = i.UniteFkNavigation != null ? i.UniteFkNavigation.UnitNameAr : "N/A", 
                   CatNameEn = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameEn : "N/A",      
                   CatNameAr = i.CatFkNavigation != null ? i.CatFkNavigation.CatNameAr : "N/A",
                   CurrentQuantity = i.Quantities
                       .Where(q => q.ItemFk == i.ItemId)  
                       .Sum(q => q.CurrentQuantity.HasValue ? (double)q.CurrentQuantity.Value : 0)  
               })
               .OrderBy(i => i.ItemId)  
               .Skip((paging.PageNumber - 1) * paging.PageSize)  
               .Take(paging.PageSize)
               .ToListAsync();

            return itemDetailsResult;
        }


        public async Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync()
        {
            var items = await _context.Items
                .Include(i => i.UniteFkNavigation) 
                .Include(i => i.CatFkNavigation)    
                .Include(i => i.Quantities)         
                .ToListAsync();

            var result = items.Select(item => new ItemDetailsDto
            {
                ItemId = item.ItemId,
                ItemNameEn = item.ItemNameEn,
                ItemNameAr = item.ItemNameAr,
                ItemCode = item.ItemCode,
                UnitNameEn = item.UniteFkNavigation?.UnitNameEn,
                UnitNameAr = item.UniteFkNavigation.UnitNameAr,
                CatNameEn = item.CatFkNavigation?.CatNameEn,
                CatNameAr = item.CatFkNavigation.CatNameAr,
                CurrentQuantity = item.Quantities.FirstOrDefault()?.CurrentQuantity ?? 0
            }).ToList();

            return result;
        }

        public async Task<List<ItemDetailsDto>> GetItemsByKeywordForChatbotAsync(string keyword)
        {
            var items = await _context.Items
                .Include(i => i.UniteFkNavigation)  
                .Include(i => i.CatFkNavigation) 
                .Include(i => i.Quantities)         
                .Where(i => EF.Functions.Like(i.ItemNameEn, $"%{keyword}%") || EF.Functions.Like(i.ItemCode, $"%{keyword}%"))
                .ToListAsync();

            var result = items.Select(item => new ItemDetailsDto
            {
                ItemId = item.ItemId,
                ItemNameEn = item.ItemNameEn,
                ItemNameAr = item.ItemNameAr,
                ItemCode = item.ItemCode,
                UnitNameEn = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameEn : "N/A",
                UnitNameAr = item.UniteFkNavigation != null ? item.UniteFkNavigation.UnitNameAr : "N/A",
                CatNameEn = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameEn : "N/A",
                CatNameAr = item.CatFkNavigation != null ? item.CatFkNavigation.CatNameAr : "N/A",

                CurrentQuantity = item.Quantities.FirstOrDefault()?.CurrentQuantity.HasValue == true
                                ? item.Quantities.FirstOrDefault()?.CurrentQuantity.Value  : 0 
            }).OrderBy(i => i.ItemId)  
            .ToList();

            return result;
        }

        public async Task<string> UpdateItem(int id, CreateItemDto item)
        {
            var existingItem = await _context.Items.FindAsync(id);

            if (existingItem == null)
            {
                return $"Item with ID {id} not found";
            }

            if (!string.IsNullOrEmpty(item.ItemCode))
            {
                var isCodeUsed = await _context.Items.AnyAsync(i => i.ItemCode == item.ItemCode && i.ItemId != id);
                if (isCodeUsed)
                {
                    return $"An item with code {item.ItemCode} already exists.";
                }
                existingItem.ItemCode = item.ItemCode;
            }

            if (!string.IsNullOrEmpty(item.ItemNameEn))
            {
                existingItem.ItemNameEn = item.ItemNameEn;
            }

            if (!string.IsNullOrEmpty(item.ItemNameAr))
            {
                existingItem.ItemNameAr = item.ItemNameAr;
            }

            if (item.CatFk != 0)
            {
                existingItem.CatFk = item.CatFk;
            }

            if (item.UniteFk != 0)
            {
                existingItem.UniteFk = item.UniteFk;
            }

            if (item.ItemExperationdate.HasValue)
            {
                existingItem.ItemExperationdate = item.ItemExperationdate.Value;
            }

            await _context.SaveChangesAsync();

            return "Item updated successfully";
        }
    }
}
