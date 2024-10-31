using Standard.DTOs.ReportDtos;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ReportRepo
{
    public interface IReportRepository
    {
        Task<List<ItemsQuantities>> getAllItemsQuantitiesInAllSubs();
        Task<List<ItemsQuantities>> getAllItemsQuantitiesBySubId(int subId);

    }
}
