using AutoMapper;
using DotnetBackend.Data;
using DotnetBackend.DTO;
using DotnetBackend.Models;
using DotnetBackend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBackend.Controllers
{
    /// Controller responsible for handling administrative operations in the Farmers Market application.
    [ApiController]
    [Route("admin")]
    [EnableCors("AllowMultipleOrigins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly FarmersmarketContext _dbContext;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, FarmersmarketContext dbContext, IMapper mapper)
        {
            _adminService = adminService;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// Adds a new farmer to the system.
        [HttpPost("newfarmer")]
        public ActionResult<Farmer> AddNewFarmer([FromBody] FarmerDTO farmer)
        {
            Farmer destinationObject = _mapper.Map<Farmer>(farmer);
            _adminService.AddFarmer(destinationObject);
            return Ok(farmer);
        }

        /// Deletes a farmer from the system.
        [HttpGet("removefarmer/{farmerid}")]
        public ActionResult<string> DeleteFarmer(int farmerid)
        {
            _adminService.RemoveFarmer(farmerid);
            return Ok("Farmer Removed Successfully");
        }

        /// Updates an existing farmer's details.
        [HttpPut("updatefarmer/{farmerid}")]
        public IActionResult UpdateFarmer(int farmerid, [FromBody] FarmerDTO f)
        {
            Farmer farmer = _adminService.GetFarmerDetails(farmerid);
            //fetch existing data
            f.FarmerId = farmerid;

            if (farmer != null)
            {
                //DTO to Farmer conversion
                Farmer destinationObject = _mapper.Map<Farmer>(f);


                _adminService.UpdateFarmer(destinationObject);

                return Ok(destinationObject);
            }

            return NotFound();
        }

        /// Retrieves the list of all categories.
        [HttpGet("categorylist")]
        public ActionResult<IEnumerable<Category>> GetCategoryList()
        {
            var categories = _adminService.GetAllCategory();
            return Ok(categories);
        }

        /// Adds a new category to the system.
        [HttpGet("addcategory/{categoryName}")]
        public IActionResult AddCategory(string categoryName)
        {
            try
            {
                _adminService.SetCategory(categoryName);

                return Ok($"New Category: {categoryName} Added");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }

        /// Adds a new product for a specific farmer.
        [HttpPost("newproduct/{farmerid}")]
        public ActionResult<string> AddNewProduct(int farmerid, [FromBody] StockDetailDTO product)
        {
            StockDetail destinationObject = _mapper.Map<StockDetail>(product);

            destinationObject.FarmerId = farmerid;

            if (product.CategoryId.HasValue)
            {
                var cat = _adminService.GetCategory(product.CategoryId.Value);
                if (cat == null)
                {
                    throw new Exception("Invalid Category");
                }

                destinationObject.CategoryId = product.CategoryId;
            }


            _adminService.AddProduct(farmerid, destinationObject);
            return Ok("Product Added Successfully");
        }

        /// Updates an existing product.
        [HttpPut("updateproduct/{productid}")]
        public ActionResult<string> UpdateProduct(int productid, [FromForm] StockDetail product)
        {
            try
            {
                product.ProductId = productid;
                _adminService.UpdateProduct(product);
                return Ok("Product Updated");
            }
            catch (Exception ex)
            {
                return NotFound($"Product with ID {productid} not found. {ex.Message}");
            }
        }

        /// Retrieves all orders in the system.
        [HttpGet("allorders")]
        public ActionResult<IEnumerable<OrderDetail>> GetAllOrders()
        {
            var orders = _adminService.GetAllOrders();
            return Ok(orders);
        }

        /// Retrieves all users in the system.
        [HttpGet("allusers")]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {

            List<User> users = _adminService.GetAllUser();

            var destinationObject = _mapper.Map<List<UserDTO>>(users);

            return Ok(users);
        }

        /// Deletes a product from the system.
        [HttpGet("removeproduct/{productid}")]
        public ActionResult<string> DeleteProduct(int productid)
        {
            _adminService.RemoveProduct(productid);
            return Ok("Product Removed Successfully");
        }

        /// Retrieves a category by its ID.
        [HttpGet("getcategory/{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {
                return Ok(_adminService.GetCategoryById(categoryId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }

    }
}