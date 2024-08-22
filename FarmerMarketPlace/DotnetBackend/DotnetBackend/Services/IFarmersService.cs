using DotnetBackend.Models;
using System.Collections.Generic;

namespace DotnetBackend.Services
{
    public interface IFarmersService
    {
        List<Farmer> GetFarmersList();
        Farmer GetFarmerDetails(int id);
        List<StockDetail> GetFarmerStock(int farmerId);
        StockDetail GetProductDetails(int farmerId, int productId);
        List<StockDetail> GetAllProduct();
    }
}
