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
        Task<List<Item>> GetItems();
        Task<Item?> GetItemById(int id);
        Task<List<Item>> GetItemsByCategoryId(int catId);
        Task<List<Item>> GetItemsBySubWHId(int subId);
        Task<List<Item>> GetItemsByUnitId(int unitId);


        //Task<Item?> GetItemById(int id);
        Task<List<ItemDetailsDto>> GetAllItemsWithDetailsAsync();
    }
}
