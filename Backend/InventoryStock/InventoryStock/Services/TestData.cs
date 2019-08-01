using InventoryStock.InventoryObject;
using Library.Api.InventoryContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace InventoryStock.Services
{
    public class TestData 
    {
        public void AddTestData(InventoryContext context)
        {
            var invent = new List<Inventory>
            {
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            new Inventory{productName="Carson",productCode="Alexander",releaseDate=DateTime.Parse("2005-09-01"),description="test1"},
            };

            invent.ForEach(s => context.Inventory.Add(s));
            context.SaveChanges();
           
        }
    }
}

