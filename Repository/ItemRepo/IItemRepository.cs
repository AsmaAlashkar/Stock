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
        //Task<Item?> GetItemById(int id);
    }
}
