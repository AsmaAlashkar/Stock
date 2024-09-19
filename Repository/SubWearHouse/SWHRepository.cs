using Microsoft.EntityFrameworkCore;
using Repository.MainWearHouse;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SubWearHouse
{
    public class SWHRepository : ISWHRepository
    {
        private readonly StockContext _context;
        public SWHRepository(StockContext context)
        {
            _context = context;
        }


        public async Task<List<SubWearhouse>> GetAllSubWearHouse()
        {
            return await _context.SubWearhouses
                .Where(mw => mw.Delet == false || mw.Delet == null)
                .ToListAsync();
        }

        public async Task<SubWearhouse?> GetSubWearHouseById(int id)
        {
            return await _context.SubWearhouses
                .Where(mw => mw.Delet == false || mw.Delet == null)
                .FirstOrDefaultAsync(mw => mw.SubId == id);
        }

        public async Task<List<SubWearHouseDTO>> GetSubNamesAndParentIdsByMainFk(int mainFk)
        {
            return await _context.SubWearhouses
                .Where(sw => sw.MainFk == mainFk && (sw.Delet == false || sw.Delet == null) && sw.ParentSubWearhouseId != null)
                .Select(sw => new SubWearHouseDTO
                {
                    SubName = sw.SubName,
                    ParentSubWearhouseId = sw.ParentSubWearhouseId
                })
                .ToListAsync(); 
        }



    }
}
