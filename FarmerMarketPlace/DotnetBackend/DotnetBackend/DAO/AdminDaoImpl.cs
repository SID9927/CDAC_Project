using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotnetBackend.Models;

using DotnetBackend.Dao;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DotnetBackend.Data;

namespace DotnetBackend.Dao
{
    public class AdminDaoImpl : IAdminDao
    {
        //private readonly FarmFreshContext _dbContext;

        //public AdminDaoImpl(FarmFreshContext dbContext)
        //{
        //    _dbContext = dbContext;
        //}

        private readonly IDbContextFactory _dbContextFactory;

        public AdminDaoImpl(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public bool AddFarmer(Farmer farmer)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                foreach (StockDetail product in farmer.StockDetails)
                {
                    product.Farmer = farmer;
                    dbContext.StockDetail.Add(product);
                }

                dbContext.Farmers.Add(farmer);
                int count = dbContext.SaveChanges();
                return count > 0;
            }
        }



        public bool AddProduct(int farmerId, StockDetail product)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                // Retrieve the farmer with the specified ID and include their stock details
                Farmer farmer = dbContext.Farmers.Include(f => f.StockDetails).FirstOrDefault(f => f.FarmerId == farmerId);



                if (farmer != null)
                {
                    // Set the association between the product and the farmer
                    product.Farmer = farmer;

                    // Add the product to the stock details
                    dbContext.StockDetail.Add(product);

                    // Save changes to the database
                    dbContext.SaveChanges();

                    return true;
                }

                return false;
            }
        }


        public bool RemoveFarmer(int farmerId)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                Farmer farmerToRemove = dbContext.Farmers
                    .Include(f => f.StockDetails)
                    .FirstOrDefault(f => f.FarmerId == farmerId);

                if (farmerToRemove != null)
                {
                    // Remove associated stock details
                    dbContext.StockDetail.RemoveRange(farmerToRemove.StockDetails);

                    // Remove the farmer
                    dbContext.Farmers.Remove(farmerToRemove);

                    // Save changes
                    dbContext.SaveChanges();

                    return true;
                }

                return false;
            }
        }


        public bool RemoveProduct(int productId)
        {
            StockDetail product = _dbContextFactory.CreateDbContext().StockDetail.Find(productId);
            if (product != null)
            {
                _dbContextFactory.CreateDbContext().StockDetail.Remove(product);
                _dbContextFactory.CreateDbContext().SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateFarmer(Farmer farmer)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                // Ensure the entity is being tracked
                dbContext.Attach(farmer);

                // Set the entity state to Modified
                dbContext.Entry(farmer).State = EntityState.Modified;

                // Save changes
                int count = dbContext.SaveChanges();

                return count > 0;
            }
        }


        public bool UpdateProduct(StockDetail product)
        {
            _dbContextFactory.CreateDbContext().Update(product);
            _dbContextFactory.CreateDbContext().SaveChanges();
            return true;
        }

        public StockDetail GetProductDetails(int productId)
        {
            return _dbContextFactory.CreateDbContext().StockDetail.Find(productId);
        }

        public Farmer GetFarmerDetails(int farmerId)
        {
            return _dbContextFactory.CreateDbContext().Farmers.Find(farmerId);
        }

        public Category GetCategory(int catId)
        {
            return _dbContextFactory.CreateDbContext().Categories.Find(catId);
        }

        public bool SetCategory(string category)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                // Check if the category already exists
                if (dbContext.Categories.Any(c => c.CategoryName == category))
                {
                    // Category already exists, handle accordingly (throw exception, return false, etc.)
                    return false;
                }

                // Create a new category entity
                Category newCategory = new Category { CategoryName = category };

                // Add the new category to the DbContext
                dbContext.Categories.Add(newCategory);

                // Save changes to the database
                dbContext.SaveChanges();

                // Return true to indicate success
                return true;
            }
        }


        public bool RemoveCategory(int catId)
        {
            Category category = _dbContextFactory.CreateDbContext().Categories.Find(catId);
            if (category != null)
            {
                _dbContextFactory.CreateDbContext().Categories.Remove(category);
                _dbContextFactory.CreateDbContext().SaveChanges();
                return true;
            }
            return false;
        }

        public string SaveImage(int productId, IFormFile imgFile)
        {
            StockDetail product = _dbContextFactory.CreateDbContext().StockDetail.Find(productId);
            if (product != null)
            {
                string path = $"wwwroot/images/{productId}_{Path.GetRandomFileName()}";
                product.ProductImage = path;
                using (FileStream fileStream = File.Create(path))
                {
                    imgFile.CopyTo(fileStream);
                }
                _dbContextFactory.CreateDbContext().SaveChanges();
                return "File copied";
            }
            return "Product not found";
        }

        public byte[] RestoreImage(int productId)
        {
            StockDetail product = _dbContextFactory.CreateDbContext().StockDetail.Find(productId);
            if (product != null)
            {
                string path = product.ProductImage;
                if (path != null)
                    return File.ReadAllBytes(path);
            }
            return null;
        }

        public List<Category> GetAllCategory()
        {
            return _dbContextFactory.CreateDbContext().Categories.ToList();
        }

        public List<OrderDetail> GetAllOrders()
        {
            return _dbContextFactory.CreateDbContext().OrderDetails.ToList();
        }

        public List<User> GetAllUser()
        {
            return _dbContextFactory.CreateDbContext().Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _dbContextFactory.CreateDbContext().Update(user);
            _dbContextFactory.CreateDbContext().SaveChanges();
            return true;
        }

        public Category GetCategoryById(int id)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                var category = dbContext.Categories.FirstOrDefault(yes => yes.CategoryId== id);
                return category;
            }
        }
    }
}
