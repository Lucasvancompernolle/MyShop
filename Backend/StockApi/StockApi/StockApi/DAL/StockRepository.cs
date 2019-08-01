using StockApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApi.DAL
{
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _context;
        //private readonly IPropertyMappingService _propertyMappingService;
        public StockRepository(StockContext context)
        {
            _context = context;
            //_propertyMappingService = propertyMappingService;
        }

        public void AddStockArt(Stockart stockart)
        {
            _context.Stockart.Add(stockart);
        }

        public void DeleteStockArt(Stockart stockart)
        {
            _context.Stockart.Remove(stockart);
        }

        public IEnumerable<Stockart> GetStockByCode(string code)
        {
            return _context.Stockart
                .Where(s => s.Code.ToUpper().Contains(code.ToUpper()))
                .OrderBy(a => a.MakeName)
                .ThenBy(a => a.Code)
                .ToList();
        }

        
        public Stockart GetStockById(int Id)
        {
            return _context.Stockart.FirstOrDefault(s => s.Id == Id);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateStockart(Stockart stockart)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stockart> GetStockArts()
        {
            return _context.Stockart
                 .OrderBy(a => a.MakeName)
                 .ThenBy(a => a.Code)
                 .ToList();
        }
    }
}
