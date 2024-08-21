using System.Threading.Tasks;
using DotnetBackend.Data;
using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Data
{
  
    public class StockDetailsRepository : IStockDetailsRepository
    {
        private readonly IDbContextFactory _dbContextFactory;

        public StockDetailsRepository(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task<StockDetail> GetByStockItemAsync(string name)
        {
            return await _dbContextFactory.CreateDbContext().StockDetail.FirstOrDefaultAsync(sd => sd.StockItem == name);
        }
    }

}
