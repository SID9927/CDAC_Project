using DotnetBackend.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetBackend.Services
{
    public interface IAdminService
    {
        bool AddFarmer(Farmer farmer);
        bool AddProduct(int farmerId, StockDetail product);
        bool RemoveFarmer(int farmerId);
        bool RemoveProduct(int productId);
        bool UpdateProduct(StockDetail product);
        bool UpdateFarmer(Farmer farmer);
        Farmer GetFarmerDetails(int farmerId);
        StockDetail GetProductDetails(int productId);
        Category GetCategory(int catId);
        bool SetCategory(string category);
        bool RemoveCategory(int catId);
        Task<string> SaveImage(int productId, IFormFile imgFile);
        Task<byte[]> RestoreImage(int empId);
        List<Category> GetAllCategory();
        List<OrderDetail> GetAllOrders();
        List<User> GetAllUser();
        bool UpdateUser(User user);
        Task<byte[]> RestoreImageAgain(string productName);

        public Category GetCategoryById(int id);
    }
}
