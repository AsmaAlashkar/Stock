using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MainWearHouse
{
    public interface IMWHRepository
    {
        Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllMainWearHouse();
        Task<ViewMainWearhouseWithSubWearhouseHierarchy?> GetMainWearHouseById(int id);
    }
}
