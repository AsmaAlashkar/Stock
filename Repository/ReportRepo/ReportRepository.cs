using Microsoft.EntityFrameworkCore;
using Standard.DTOs.ReportDtos;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ReportRepo
{
    public class ReportRepository:IReportRepository
    {
        private readonly StockContext _context;

        public ReportRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<List<ItemsQuantities>> getAllItemsQuantitiesInAllSubs()
        {
            var itemQuantities = await _context.Quantities
                .Include(q => q.ItemFkNavigation)
                .Select(qi => new ItemsQuantities
                {
                    ItemName = qi.ItemFkNavigation.ItemNameEn,
                    CurrentQuantity = qi.CurrentQuantity
                })
                .ToListAsync();
            return itemQuantities;
        }

        public async Task<List<ItemsQuantities>> getAllItemsQuantitiesBySubId(int subId)
        {
            var itemQuantities = await _context.SubItems
                .Include(q => q.ItemFkNavigation)
                .Where(s=>s.SubFk ==  subId)    
                .Select(qi => new ItemsQuantities
                {
                    ItemName = qi.ItemFkNavigation.ItemNameEn,
                    CurrentQuantity = qi.Quantity
                })
                .ToListAsync();
            return itemQuantities;
        }
    }
}
