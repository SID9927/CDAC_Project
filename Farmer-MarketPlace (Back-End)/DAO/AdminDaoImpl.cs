using DotnetBackend.Data;
using DotnetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetBackend.Dao
{
    public class AdminDaoImpl : IAdminDao
    {
        private readonly FarmersmarketContext _dbContext;

        public AdminDaoImpl(FarmersmarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddFarmer(Farmer farmer)
        {
            foreach (StockDetail product in farmer.StockDetails)
            {
                product.Farmer = farmer;
                _dbContext.StockDetail.Add(product);
            }

            _dbContext.Farmers.Add(farmer);
            int count = _dbContext.SaveChanges();
            return count > 0;
        }

        public bool AddProduct(int farmerId, StockDetail product)
        {
            // Retrieve the farmer with the specified ID and include their stock details
            Farmer farmer = _dbContext.Farmers.Include(f => f.StockDetails).FirstOrDefault(f => f.FarmerId == farmerId);

            if (farmer != null)
            {
                // Set the association between the product and the farmer
                product.Farmer = farmer;

                // Add the product to the stock details
                _dbContext.StockDetail.Add(product);

                // Save changes to the database
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public bool RemoveFarmer(int farmerId)
        {
            Farmer farmerToRemove = _dbContext.Farmers
                .Include(f => f.StockDetails)
                .FirstOrDefault(f => f.FarmerId == farmerId);

            if (farmerToRemove != null)
            {
                // Remove associated stock details
                _dbContext.StockDetail.RemoveRange(farmerToRemove.StockDetails);

                // Remove the farmer
                _dbContext.Farmers.Remove(farmerToRemove);

                // Save changes
                _dbContext.SaveChanges();

                return true;
            }

            return false;
        }

        public bool RemoveProduct(int productId)
        {
            StockDetail product = _dbContext.StockDetail.Find(productId);
            if (product != null)
            {
                _dbContext.StockDetail.Remove(product);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdateFarmer(Farmer updatedFarmer)
        {
            var existingFarmer = _dbContext.Farmers.Find(updatedFarmer.FarmerId);
            if (existingFarmer == null)
            {
                return false;
            }

            _dbContext.Entry(existingFarmer).CurrentValues.SetValues(updatedFarmer);
            _dbContext.SaveChanges();
            return true;
        }


        public bool UpdateProduct(StockDetail product)
        {
            try
            {
                var existingProduct = _dbContext.StockDetail.Find(product.ProductId);
                if (existingProduct != null)
                {
                    // Update only the properties that are not null in the incoming product
                    if (product.PricePerUnit.HasValue)
                        existingProduct.PricePerUnit = product.PricePerUnit;
                    if (product.Quantity != 0)
                        existingProduct.Quantity = product.Quantity;
                    if (!string.IsNullOrEmpty(product.StockItem))
                        existingProduct.StockItem = product.StockItem;
                    if (product.CategoryId.HasValue)
                        existingProduct.CategoryId = product.CategoryId;
                    if (product.FarmerId.HasValue)
                        existingProduct.FarmerId = product.FarmerId;
                    if (!string.IsNullOrEmpty(product.ProductImage))
                        existingProduct.ProductImage = product.ProductImage;

                    _dbContext.Entry(existingProduct).State = EntityState.Modified;
                    int count = _dbContext.SaveChanges();
                    Console.WriteLine($"Updated {count} records");
                    return count > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                return false;
            }
        }

        public StockDetail GetProductDetails(int productId)
        {
            return _dbContext.StockDetail.Find(productId);
        }

        public Farmer GetFarmerDetails(int farmerId)
        {
            return _dbContext.Farmers.Find(farmerId);
        }

        public Category GetCategory(int catId)
        {
            return _dbContext.Categories.Find(catId);
        }

        public bool SetCategory(string category)
        {
            // Check if the category already exists
            if (_dbContext.Categories.Any(c => c.CategoryName == category))
            {
                // Category already exists, handle accordingly (throw exception, return false, etc.)
                return false;
            }

            // Create a new category entity
            Category newCategory = new Category { CategoryName = category };

            // Add the new category to the DbContext
            _dbContext.Categories.Add(newCategory);

            // Save changes to the database
            _dbContext.SaveChanges();

            // Return true to indicate success
            return true;
        }

        public bool RemoveCategory(int catId)
        {
            Category category = _dbContext.Categories.Find(catId);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public string SaveImage(int productId, IFormFile imgFile)
        {
            StockDetail product = _dbContext.StockDetail.Find(productId);
            if (product != null)
            {
                string path = $"wwwroot/images/{productId}_{Path.GetRandomFileName()}";
                product.ProductImage = path;
                using (FileStream fileStream = File.Create(path))
                {
                    imgFile.CopyTo(fileStream);
                }
                _dbContext.SaveChanges();
                return "File copied";
            }
            return "Product not found";
        }

        public byte[] RestoreImage(int productId)
        {
            StockDetail product = _dbContext.StockDetail.Find(productId);
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
            return _dbContext.Categories.ToList();
        }

        public List<OrderDetail> GetAllOrders()
        {
            return _dbContext.OrderDetails.ToList();
        }

        public List<User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _dbContext.Update(user);
            _dbContext.SaveChanges();
            return true;
        }

        public Category GetCategoryById(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(yes => yes.CategoryId == id);
            return category;
        }
    }
}