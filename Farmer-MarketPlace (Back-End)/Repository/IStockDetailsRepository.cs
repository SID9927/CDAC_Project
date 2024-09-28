using DotnetBackend.Models;
namespace FarmFresh.Data;
public interface IStockDetailsRepository
{
    Task<StockDetail> GetByStockItemAsync(string name);
}
