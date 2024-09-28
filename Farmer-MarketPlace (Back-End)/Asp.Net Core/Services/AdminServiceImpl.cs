using DotnetBackend.Dao;
using DotnetBackend.Data;
using DotnetBackend.ExceptionHandler;
using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Services
{
    public class AdminServiceImpl : IAdminService
    {
        private readonly IAdminDao _adminDao;
        private readonly FarmersmarketContext _dbContext;

        public AdminServiceImpl(IAdminDao adminDao, FarmersmarketContext dbContext)
        {
            _adminDao = adminDao;
            _dbContext = dbContext;
        }

        public bool AddFarmer(Farmer farmer)
        {
            return _adminDao.AddFarmer(farmer);
        }

        public bool AddProduct(int farmerId, StockDetail product)
        {
            return _adminDao.AddProduct(farmerId, product);
        }

        public bool RemoveFarmer(int farmerId)
        {
            return _adminDao.RemoveFarmer(farmerId);
        }

        public bool RemoveProduct(int productId)
        {
            return _adminDao.RemoveProduct(productId);
        }

        public bool UpdateProduct(StockDetail product)
        {
            return _adminDao.UpdateProduct(product);
        }

        public bool UpdateFarmer(Farmer farmer)
        {
            return _adminDao.UpdateFarmer(farmer);
        }

        public Farmer GetFarmerDetails(int farmerId)
        {
            return _adminDao.GetFarmerDetails(farmerId);
        }

        public StockDetail GetProductDetails(int productId)
        {
            return _adminDao.GetProductDetails(productId);
        }

        public Category GetCategory(int catId)
        {
            return _adminDao.GetCategory(catId);
        }

        public bool SetCategory(string category)
        {
            return _adminDao.SetCategory(category);
        }

        public bool RemoveCategory(int catId)
        {
            return _adminDao.RemoveCategory(catId);
        }

        public async Task<string> SaveImage(int productId, IFormFile imgFile)
        {
            var product = _adminDao.GetProductDetails(productId);
            if (product == null)
            {
                throw new ResourceNotFoundException($"Product with ID {productId} not found.");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", imgFile.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imgFile.CopyToAsync(stream);
            }

            product.ProductImage = filePath;
            _adminDao.UpdateProduct(product);

            return filePath;
        }

        public async Task<byte[]> RestoreImage(int empId)
        {
            var product = _adminDao.GetProductDetails(empId);
            if (product == null || string.IsNullOrEmpty(product.ProductImage))
            {
                throw new ResourceNotFoundException($"Image not yet assigned for product with ID {empId}.");
            }

            return await File.ReadAllBytesAsync(product.ProductImage);
        }

        public List<Category> GetAllCategory()
        {
            return _adminDao.GetAllCategory();
        }

        public List<OrderDetail> GetAllOrders()
        {
            return _adminDao.GetAllOrders();
        }

        public List<User> GetAllUser()
        {
            return _adminDao.GetAllUser();
        }

        public bool UpdateUser(User user)
        {
            return _adminDao.UpdateUser(user);
        }

        public async Task<byte[]> RestoreImageAgain(string productName)
        {
            var product = await _dbContext.StockDetail.FirstOrDefaultAsync(s => s.StockItem == productName);
            if (product == null || string.IsNullOrEmpty(product.ProductImage))
            {
                throw new ResourceNotFoundException($"Image not yet assigned for product with name {productName}.");
            }

            return await File.ReadAllBytesAsync(product.ProductImage);
        }

        public Category GetCategoryById(int id)
        {
            return _adminDao.GetCategoryById(id);
        }
    }
}
