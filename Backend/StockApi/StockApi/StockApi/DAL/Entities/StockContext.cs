using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApi.DAL.Entities
{
    public class StockContext :DbContext
    {
        public StockContext(DbContextOptions<StockContext> options)
         : base(options)
        {
           // Database.Migrate();
        }

        public DbSet<Stockart> Stockart { get; set; }
    }
}
