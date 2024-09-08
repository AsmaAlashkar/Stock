﻿using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.VMainWearhouseItem
{
    public interface IVWHIRepository
    {
        Task<List<ViewWearhouseItem>> GetAllMainWearHouse();

        Task<ViewWearhouseItem?> GetMainWearHouseById(int id);
        Task<List<ViewWearhouseItem>> GetAllSubWearHouse();
        Task<ViewWearhouseItem?> GetSubWearHouseById(int id);


    }
}