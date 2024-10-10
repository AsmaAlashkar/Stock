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
        Task<Item?> GetItemById(int id);
        Task<List<ItemDetailsDto>> GetItemsByCategoryId(int catId, DTOPaging paging);
        Task<List<ItemDetailsDto>> GetItemsBySubWHId(int subId, DTOPaging paging);
        Task<List<Item>> GetItemsByUnitId(int unitId);


        //Task<Item?> GetItemById(int id);
        Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync();
    }
}
