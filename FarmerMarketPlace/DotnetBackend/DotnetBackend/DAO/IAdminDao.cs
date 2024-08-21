using System.Collections.Generic;
using DotnetBackend.Models;
using Microsoft.AspNetCore.Http;

namespace DotnetBackend.Dao
{
    public interface IAdminDao
    {
        bool AddFarmer(Farmer farmer);
        bool AddProduct(int farmerId, StockDetail product);
        bool RemoveFarmer(int farmerId);
        bool RemoveProduct(int productId);
        bool UpdateFarmer(Farmer farmer);
        bool UpdateProduct(StockDetail product);
        StockDetail GetProductDetails(int productId);
        Farmer GetFarmerDetails(int farmerId);
        Category GetCategory(int categoryId);
        bool SetCategory(string category);
        bool RemoveCategory(int categoryId);
        string SaveImage(int productId, IFormFile imgFile);
        byte[] RestoreImage(int productId);
        List<Category> GetAllCategory();
        List<OrderDetail> GetAllOrders();
        List<User> GetAllUser();
        bool UpdateUser(User user);

        public Category GetCategoryById(int id);
    }
}
