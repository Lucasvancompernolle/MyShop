using InventoryStock.InventoryObject;
using InventoryStock.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private InventoryRepository _inventoryRepository;

        [Fact]
        public void Test1()
        {
            string code = "alex";
            IEnumerable<Inventory> authorsFromRepo = _inventoryRepository.GetInventory(code);
            Assert.Equal("dd", code);
        }
    }
}
