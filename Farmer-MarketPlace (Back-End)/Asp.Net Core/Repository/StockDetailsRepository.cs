using DotnetBackend.Data;
using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Data
{
    public class StockDetailsRepository : IStockDetailsRepository
    {
        private readonly FarmersmarketContext _dbContext;

        public StockDetailsRepository(FarmersmarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StockDetail> GetByStockItemAsync(string name)
        {
            return await _dbContext.StockDetail.FirstOrDefaultAsync(sd => sd.StockItem == name);
        }
    }
}
