using Standard.DTOs.ItemDtos;
using Standard.DTOs.UnitDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitRepo
{
    public interface IUnitRepository
    {
        Task<List<DisplayUnitNameDto>> GetUnitsNames();
    }
}
