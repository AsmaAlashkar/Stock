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

        public async Task<List<MainWearhouse>> GetAllMainWearHouse()
        {
            return await _context.MainWearhouses
                .Where(mw => mw.Delet == false || mw.Delet == null)
                .ToListAsync();
        }

        public async Task<MainWearhouse?> GetMainWearHouseById(int id)
        {
            return await _context.MainWearhouses
                .Where(mw => mw.Delet == false || mw.Delet == null)
                .FirstOrDefaultAsync(mw => mw.MainId == id);
        }

        




    }
}
