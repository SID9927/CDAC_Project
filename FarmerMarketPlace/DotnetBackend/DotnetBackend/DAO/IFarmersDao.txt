using DotnetBackend.Models;

namespace DotnetBackend.Dao
{
    public interface IFarmersDao
    {
        List<StockDetail> GetAllProduct();
        List<Farmer> GetAllFarmers();
        Farmer GetFarmerDetails(int id);
        List<StockDetail> GetFarmerStock(int farmerId);
        StockDetail GetProductDetails(int farmerId, int productId);
    }
}
