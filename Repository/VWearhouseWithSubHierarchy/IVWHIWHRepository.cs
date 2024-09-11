using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VWearhouseWithSubHierarchy
{
    public interface IVWHIWHRepository
    {

        Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllMainWearHouse();
        Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetMainWearHouseById(int mainId);
        Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllSubWearHouse();
        Task<ViewMainWearhouseWithSubWearhouseHierarchy?> GetSubWearHouseById(int id);
        Task<List<ViewMainWearhouseWithSubWearhouseHierarchy>> GetAllSubByMainId(int mainId);
    }
}
