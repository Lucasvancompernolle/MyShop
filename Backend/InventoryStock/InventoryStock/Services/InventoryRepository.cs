using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryStock.InventoryObject;
using Library.Api.InventoryContext;

namespace InventoryStock.Services
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository(InventoryContext context)
        {
            _context = context;
            TestData testData = new TestData();
            testData.AddTestData(context);
        }

        public void AddArticle(Inventory article)
        {
            _context.Add(article);
        }

        public IEnumerable<Inventory> GetInventory(string code)
        {
            return _context.Inventory
                  .Where(a => a.productCode.ToUpper().Contains(code.ToUpper()))
                  .OrderBy(a => a.productName)
                  .ThenBy(a => a.releaseDate)
                  .ToList();    
        }
    }
}
