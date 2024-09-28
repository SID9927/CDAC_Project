using DotnetBackend.Data;
using DotnetBackend.Models;

namespace DotnetBackend.Dao
{
    public class FarmersDaoImpl : IFarmersDao
    {
        private readonly FarmersmarketContext _dbContext;

        public FarmersDaoImpl(FarmersmarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Farmer> GetAllFarmers()
        {
            return _dbContext.Farmers
                .Select(f => new Farmer
                {
                    FarmerId = f.FarmerId,
                    Firstname = f.Firstname,
                    Lastname = f.Lastname,
                    Email = f.Email,
                    PhoneNo = f.PhoneNo,
                    Address = f.Address
                })
            .ToList();
        }

        public List<StockDetail> GetFarmerStock(int farmerId)
        {
            return _dbContext.StockDetail
                .Where(sd => sd.FarmerId == farmerId)
                .Select(sd => new StockDetail
                {
                    ProductId = sd.ProductId,
                    StockItem = sd.StockItem,
                    PricePerUnit = sd.PricePerUnit
                })
            .ToList();
        }

        public StockDetail GetProductDetails(int farmerId, int productId)
        {
            return _dbContext.StockDetail
                .Where(sd => sd.FarmerId == farmerId && sd.ProductId == productId)
                .Select(sd => new StockDetail
                {
                    ProductId = sd.ProductId,
                    StockItem = sd.StockItem,
                    Quantity = sd.Quantity,
                    PricePerUnit = sd.PricePerUnit,
                    Category = sd.Category
                })
                .FirstOrDefault();
        }

        public Farmer GetFarmerDetails(int id)
        {
            return _dbContext.Farmers
                .Where(f => f.FarmerId == id)
                .Select(f => new Farmer
                {
                    FarmerId = f.FarmerId,
                    Firstname = f.Firstname,
                    Lastname = f.Lastname,
                    Email = f.Email,
                    PhoneNo = f.PhoneNo,
                    Address = f.Address
                })
            .FirstOrDefault();
        }

        public List<StockDetail> GetAllProduct()
        {
            return _dbContext.StockDetail
                .ToList();
        }
    }
}