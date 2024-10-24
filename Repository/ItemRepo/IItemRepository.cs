using Standard.DTOs;
using Standard.DTOs.ItemDtos;
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
        Task<ItemDetailsResult> GetItems(DTOPaging paging);
        Task<List<ItemsNamesDto>> GetItemsNames();
        Task<List<ItemsNamesDto>> GetItemsNamesBySubId(int subId);
        Task<ItemDetailsDto?> GetItemById(int id);
        Task<ItemDetailsResult> GetItemsByCategoryId(int catId, DTOPaging paging);
        Task<ItemDetailsResult> GetItemsBySubWHId(int subId, DTOPaging paging);
        Task<ItemDetailsResult> GetItemsByUnitId(int unitId, DTOPaging paging);
        Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync();

    }
}
