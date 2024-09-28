using DotnetBackend.Dao; // Replace with the actual namespace of your models
using DotnetBackend.Data;
using DotnetBackend.Models;

namespace DotnetBackend.Services;
public class FarmersServiceImpl : IFarmersService
{
    private readonly IFarmersDao _farmersDao;
    private readonly FarmersmarketContext _dbContext;

    public FarmersServiceImpl(IFarmersDao farmersDao, FarmersmarketContext dbContext)
    {
        _farmersDao = farmersDao;
        _dbContext = dbContext;
    }

    public List<Farmer> GetFarmersList()
    {
        return _farmersDao.GetAllFarmers();
    }

    public List<StockDetail> GetFarmerStock(int farmerId)
    {
        return _farmersDao.GetFarmerStock(farmerId);
    }

    public StockDetail GetProductDetails(int farmerId, int productId)
    {
        return _farmersDao.GetProductDetails(farmerId, productId);
    }

    public Farmer GetFarmerDetails(int id)
    {
        return _farmersDao.GetFarmerDetails(id);
    }

    public List<StockDetail> GetAllProduct()
    {
        return _farmersDao.GetAllProduct();
    }
}
