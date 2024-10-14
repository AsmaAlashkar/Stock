using Microsoft.EntityFrameworkCore;
using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.PermissionType
{
    public class PermissionTypeRepository :IPermissionTypeRepository
    {
        private readonly StockContext _context;
        public PermissionTypeRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<List<Standard.Entities.PermissionType>> GetPermissionsTypes()
        {
            // Fetch PermissionType entities and map them to DTOs
            return await _context.PermissionTypes.ToListAsync();
        }

        
    }
}
