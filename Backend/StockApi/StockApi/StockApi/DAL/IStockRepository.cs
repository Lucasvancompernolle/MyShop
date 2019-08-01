using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockApi.DAL
{
    public interface IStockRepository
    {
        bool Save();
        IEnumerable<Stockart> GetStockByCode(string code);
        IEnumerable<Stockart> GetStockArts();

        Stockart GetStockById(int Id);
        void AddStockArt(Stockart stockart);
        void UpdateStockart(Stockart stockart);
        void DeleteStockArt(Stockart stockart);
    }
}
