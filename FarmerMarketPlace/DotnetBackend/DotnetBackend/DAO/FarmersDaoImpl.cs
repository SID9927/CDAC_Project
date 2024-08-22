using System.Collections.Generic;
using System.Linq;
using DotnetBackend.Dao;
using DotnetBackend.Data;
using DotnetBackend.Models;

using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Dao
{
    public class FarmersDaoImpl : IFarmersDao
    {
        private readonly IDbContextFactory _dbContextFactory;

        public FarmersDaoImpl(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Farmer> GetAllFarmers()
        {
            return _dbContextFactory.CreateDbContext().Farmers
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
            return _dbContextFactory.CreateDbContext().StockDetail
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
            return _dbContextFactory.CreateDbContext().StockDetail
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
            return _dbContextFactory.CreateDbContext().Farmers
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
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                return dbContext.StockDetail
                   
                    .ToList();
            }
        }

    }
}
