using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MainWearHouse
{
    public class MWHRepository : IMWHRepository
    {
        private readonly StockContext _context;
        public MWHRepository(StockContext context)
        {
            _context = context;
        }


        public async Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllMainWearHouse()
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(mw => mw.Md == false || mw.Md == null)
                .ToListAsync();
        }


        public async Task<ViewMainWearhouseWithSubWearhouseHierarchy?> GetMainWearHouseById(int id)
        {
            return await _context.ViewMainWearhouseWithSubWearhouseHierarchies
                .Where(mw => mw.Md == false || mw.Md == null)
                .FirstOrDefaultAsync(mw => mw.MainId == id);
        }

    }
}
