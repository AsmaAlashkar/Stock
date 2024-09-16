using Microsoft.EntityFrameworkCore;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VWearhouseWithSubHierarchy
{
    public class VWHIWHRepository : IVWHIWHRepository
    {
        private readonly StockContext _context;
        public VWHIWHRepository(StockContext context)
        {
            _context = context;
        }
        public async Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllMainWearHouse()
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(mw => mw.Md == false || mw.Md == null)
                .GroupBy(mw => mw.MainId)        // Group by MainId to eliminate duplicates
                .Select(g => g.First())          // Select the first item from each group
                .ToListAsync();
        }
        public async Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetMainWearHouseById(int mainId)
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(mw => mw.MainId == mainId && (mw.Md == false || mw.Md == null))
                .ToListAsync();
        }

        public async Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllSubWearHouse()
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(sw => sw.Sd == false || sw.Sd == null && sw.SubId != null)
                .ToListAsync();
        }

        public async Task<ViewMainWearhouseWithSubWearhouseHierarchy?> GetSubWearHouseById(int id)
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(sw => sw.Md == false || sw.Md == null && sw.SubId != null)
                .FirstOrDefaultAsync(sw => sw.SubId == id);
        }

        public async Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllSubByMainId(int mainId)
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(sw => sw.MainId == mainId && (sw.Sd == false || sw.Sd == null))
                .ToListAsync();
        }
      
    }
}
