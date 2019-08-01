using InventoryStock.InventoryObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryStock.Services
{
    public interface IInventoryRepository
    {
        IEnumerable<Inventory> GetInventory(string genre);
        void AddArticle(Inventory article);

        

    }
}
