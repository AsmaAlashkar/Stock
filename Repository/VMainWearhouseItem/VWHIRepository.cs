using Microsoft.EntityFrameworkCore;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VMainWearhouseItem
{
    public class VWHIRepository :IVWHIRepository
    {
        private readonly StockContext _context;
        public VWHIRepository(StockContext context)
        {
            _context = context;
        }

        //public async Task<List<ViewWearhouseItem>> GetAllMainWearHouse()
        //{
        //    return await _context.ViewWearhouseItems
        //        .Where(mw => mw.Md == false || mw.Md == null)
        //        .GroupBy(mw => mw.MainId)        // Group by MainId to eliminate duplicates
        //        .Select(g => g.First())          // Select the first item from each group
        //        .ToListAsync();
        //}
        //public async Task<List<ViewWearhouseItem>> GetMainWearHouseById(int mainId)
        //{
        //    return await _context.ViewWearhouseItems
        //        .Where(mw => mw.MainId == mainId && (mw.Md == false || mw.Md == null))
        //        .ToListAsync();
        //}
      
        //public async Task<List<ViewWearhouseItem>> GetAllSubWearHouse()
        //{
        //    return await _context.ViewWearhouseItems
        //        .Where(sw => sw.Sd == false || sw.Sd == null && sw.SubId != null)
        //        .ToListAsync();
        //}

        //public async Task<ViewWearhouseItem?> GetSubWearHouseById(int id)
        //{
        //    return await _context.ViewWearhouseItems
        //        .Where(sw => sw.Md == false || sw.Md == null && sw.SubId != null)
        //        .FirstOrDefaultAsync(sw => sw.SubId == id);
        //}

        //public async Task<List<ViewWearhouseItem>> GetAllSubByMainId(int mainId)
        //{
        //    return await _context.ViewWearhouseItems
        //        .Where(sw => sw.MainId == mainId && (sw.Sd == false || sw.Sd == null))
        //        .ToListAsync();
        //}
    }
}
