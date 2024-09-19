using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.SubWearHouse
{
    public interface ISWHRepository
    {
        Task<List<SubWearhouse>> GetAllSubWearHouse();
        Task<SubWearhouse?> GetSubWearHouseById(int id);
        Task<List<SubWearHouseDTO>> GetSubNamesAndParentIdsByMainFk(int mainFk);
    }
}
