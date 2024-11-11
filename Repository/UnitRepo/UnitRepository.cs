using Microsoft.EntityFrameworkCore;
using Standard.DTOs.ItemDtos;
using Standard.DTOs.UnitDtos;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitRepo
{
    public class UnitRepository : IUnitRepository
    {
        private readonly StockContext _context;

        public UnitRepository(StockContext context)
        {
            _context = context;
        }
        public async Task<List<DisplayUnitNameDto>> GetUnitsNames()
        {
            var units = await _context.Units
                             .Select(item => new DisplayUnitNameDto
                             {
                                 UnitId=item.UnitId,
                                 UnitName=item.UnitNameEn
                             })
                             .ToListAsync();
            return units;
        }
    }
}
