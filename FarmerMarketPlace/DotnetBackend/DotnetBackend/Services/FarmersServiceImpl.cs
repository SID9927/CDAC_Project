using DotnetBackend.Models;

using System.Collections.Generic;
using DotnetBackend.Models;
using DotnetBackend.Dao; // Replace with the actual namespace of your models

namespace DotnetBackend.Services;
public class FarmersServiceImpl : IFarmersService
{
    private readonly IFarmersDao _farmersRepository;

   

    public FarmersServiceImpl(IFarmersDao farmersRepository)
    {
        _farmersRepository = farmersRepository;
       
        
    }

    public List<Farmer> GetFarmersList()
    {
        return _farmersRepository.GetAllFarmers();
    }

    public List<StockDetail> GetFarmerStock(int farmerId)
    {
        return _farmersRepository.GetFarmerStock(farmerId);
    }

    public StockDetail GetProductDetails(int farmerId, int productId)
    {
        return _farmersRepository.GetProductDetails(farmerId, productId);
    }

    public Farmer GetFarmerDetails(int id)
    {
        return _farmersRepository.GetFarmerDetails(id);
    }

    public List<StockDetail> GetAllProduct()
    {
        return _farmersRepository.GetAllProduct();
    }
}
