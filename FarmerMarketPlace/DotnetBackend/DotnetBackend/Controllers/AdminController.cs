using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotnetBackend.Models;
using DotnetBackend.Data;
using DotnetBackend.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using DotnetBackend.DTO;

namespace DotnetBackend.Controllers
{
    /// <summary>
    /// Controller responsible for handling administrative operations in the Farmers Market application.
    /// </summary>
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

        /// <summary>
        /// Adds a new farmer to the system.
        /// </summary>
        /// <param name="farmer">The farmer data transfer object.</param>
        /// <returns>The newly added farmer.</returns>
        [HttpPost("newfarmer")]
        public ActionResult<Farmer> AddNewFarmer([FromBody] FarmerDTO farmer)
        {
            Farmer destinationObject = _mapper.Map<Farmer>(farmer);
            _adminService.AddFarmer(destinationObject);
            return Ok(farmer);
        }

        /// <summary>
        /// Adds a new product for a specific farmer.
        /// </summary>
        /// <param name="farmerid">The ID of the farmer.</param>
        /// <param name="product">The product data transfer object.</param>
        /// <returns>A success message.</returns>
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

        /// <summary>
        /// Uploads an image for a specific product.
        /// </summary>
        /// <param name="productid">The ID of the product.</param>
        /// <param name="imgFile">The image file to upload.</param>
        /// <returns>A success message.</returns>
        [HttpPost("{productid}/image")]
        public async Task<ActionResult<string>> UploadImage(int productid, IFormFile imgFile)
        {


            _adminService.SaveImage(productid, imgFile);


            return Ok("Image Uploaded Successfully");
        }

        /// <summary>
        /// Retrieves the image for a specific product.
        /// </summary>
        /// <param name="productid">The ID of the product.</param>
        /// <returns>The product image as a byte array.</returns>
        [HttpGet("{productid}")]
        public ActionResult<byte[]> DownloadImage(int productid)
        {
            try
            {

                return Ok(_adminService.RestoreImage(productid));
            }
            catch (Exception ex)
            {
                return NotFound($"Image for product ID {productid} not found. {ex.Message}");
            }
        }

        /// <summary>
        /// Deletes a farmer from the system.
        /// </summary>
        /// <param name="farmerid">The ID of the farmer to delete.</param>
        /// <returns>A success message.</returns>
        [HttpGet("removefarmer/{farmerid}")]
        public ActionResult<string> DeleteFarmer(int farmerid)
        {
            _adminService.RemoveFarmer(farmerid);
            return Ok("Farmer Removed Successfully");
        }

        /// <summary>
        /// Deletes a product from the system.
        /// </summary>
        /// <param name="productid">The ID of the product to delete.</param>
        /// <returns>A success message.</returns>
        [HttpGet("removeproduct/{productid}")]
        public ActionResult<string> DeleteProduct(int productid)
        {
            _adminService.RemoveProduct(productid);
            return Ok("Product Removed Successfully");
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productid">The ID of the product to update.</param>
        /// <param name="product">The updated product details.</param>
        /// <returns>A success message.</returns>
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


        /// <summary>
        /// Retrieves the list of all categories.
        /// </summary>
        /// <returns>A list of all categories.</returns>
        [HttpGet("categorylist")]
        public ActionResult<IEnumerable<Category>> GetCategoryList()
        {
            var categories = _adminService.GetAllCategory();
            return Ok(categories);
        }

        /// <summary>
        /// Retrieves all orders in the system.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        [HttpGet("allorders")]
        public ActionResult<IEnumerable<OrderDetail>> GetAllOrders()
        {
            var orders = _adminService.GetAllOrders();
            return Ok(orders);
        }

        /// <summary>
        /// Retrieves all users in the system.
        /// </summary>
        /// <returns>A list of all users.</returns>
        [HttpGet("allusers")]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {

            List<User> users = _adminService.GetAllUser();

            var destinationObject = _mapper.Map<List<UserDTO>>(users);

            return Ok(users);
        }

        /// <summary>
        /// Updates an existing farmer's details.
        /// </summary>
        /// <param name="farmerid">The ID of the farmer to update.</param>
        /// <param name="f">The updated farmer details.</param>
        /// <returns>The updated farmer object.</returns>
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

        // <summary>
        /// Adds a new category to the system.
        /// </summary>
        /// <param name="categoryName">The name of the new category.</param>
        /// <returns>A success message.</returns>
        [HttpGet("addcategory/{categoryName}")]
        public IActionResult AddCategory(string categoryName)
        {
            try
            {
                //Category destinationObject = _mapper.Map<Category>(categoryModel);

                // Validate the incoming data (categoryModel) as needed

                // Call the service method to add the category
                _adminService.SetCategory(categoryName);

                // Return a successful response
                return Ok($"New Category: {categoryName} Added");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="categoryId">The ID of the category to retrieve.</param>
        /// <returns>The requested category.</returns>
        [HttpGet("getcategory/{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {
                //Category destinationObject = _mapper.Map<Category>(categoryModel);

                // Validate the incoming data (categoryModel) as needed

                // Call the service method to add the category


                // Return a successful response
                return Ok(_adminService.GetCategoryById(categoryId));
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }

    }
}