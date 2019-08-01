using InventoryStock.InventoryObject;
using Microsoft.EntityFrameworkCore;

namespace Library.Api.InventoryContext
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
           : base(options)
        {
            Database.Migrate();
            
        }

        public DbSet<Inventory> Inventory { get; set; }

    }
}