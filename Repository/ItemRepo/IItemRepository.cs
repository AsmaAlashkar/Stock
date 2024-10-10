using Standard.DTOs;
using Standard.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ItemRepo
{
    public interface IItemRepository
    {
        Task<List<ItemDetailsDto>> GetItems(DTOPaging paging);
        Task<ItemDetailsDto?> GetItemById(int id);
        Task<List<ItemDetailsDto>> GetItemsByCategoryId(int catId, DTOPaging paging);
        Task<List<ItemDetailsDto>> GetItemsBySubWHId(int subId, DTOPaging paging);
        Task<List<ItemDetailsDto>> GetItemsByUnitId(int unitId, DTOPaging paging);
        Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync();

    }
}
