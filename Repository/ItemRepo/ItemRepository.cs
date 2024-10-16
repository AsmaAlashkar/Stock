﻿using Microsoft.EntityFrameworkCore;
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
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                }).FirstOrDefaultAsync(); ;

            return result;
        }


        public async Task<List<ItemDetailsDto>> GetItemsByCategoryId(int catId, DTOPaging paging)
        {
            var result = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                where item.CatFk == catId
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  // Use the appropriate language field
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  // Safely handle nullable types
                                })
                                .OrderBy(i => i.ItemId)  // Order by item ID or any other field
                                .Skip((paging.PageNumber - 1) * paging.PageSize)  // Skip records for previous pages
                                .Take(paging.PageSize)  // Take only the number of records for the current page
                                .ToListAsync();

            return result;
        }

        public async Task<List<ItemDetailsDto>> GetItemsBySubWHId(int subId, DTOPaging paging)
        {
            var result = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                where item.SubFk == subId
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  // Use the appropriate language field
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  // Safely handle nullable types
                                })
                                .OrderBy(i => i.ItemId)  // Order by item ID or any other field
                                .Skip((paging.PageNumber - 1) * paging.PageSize)  // Skip records for previous pages
                                .Take(paging.PageSize)  // Take only the number of records for the current page
                                .ToListAsync();  // Get the list of items with their details

            return result;
        }

        public async Task<List<ItemDetailsDto>> GetItemsByUnitId(int unitId, DTOPaging paging)
        {
            var result = await (from item in _context.Items
                                join unit in _context.Units on item.UniteFk equals unit.UnitId
                                join category in _context.Categories on item.CatFk equals category.CatId
                                join quantity in _context.Quantities on item.ItemId equals quantity.ItemFk
                                where item.UniteFk == unitId
                                select new ItemDetailsDto
                                {
                                    ItemId = item.ItemId,
                                    ItemName = item.ItemName,
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn, 
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                })
                               .OrderBy(i => i.ItemId)  
                               .Skip((paging.PageNumber - 1) * paging.PageSize) 
                               .Take(paging.PageSize)  
                               .ToListAsync(); 

            return result;
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
                                    UnitName = unit.UnitName,
                                    CategoryName = category.CatNameEn,  
                                    CurrentQuantity = (int)quantity.CurrentQuantity.GetValueOrDefault()  
                                }).ToListAsync(); 

            return result;
        }

    }
}
