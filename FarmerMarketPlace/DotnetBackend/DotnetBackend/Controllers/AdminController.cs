using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DotnetBackend.Models;
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
    [ApiController]
    [Route("admin")]
    [EnableCors("AllowMultipleOrigins")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService,IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
            
        }

        [HttpPost("newfarmer")]
        public ActionResult<Farmer> AddNewFarmer([FromBody] FarmerDTO farmer)
        {
            Farmer destinationObject = _mapper.Map<Farmer>(farmer);
            _adminService.AddFarmer(destinationObject);
            return Ok(farmer);
        }

        [HttpPost("newproduct/{farmerid}")]
        public ActionResult<string> AddNewProduct(int farmerid, [FromBody] StockDetailDTO product)
        {
            StockDetail destinationObject = _mapper.Map<StockDetail>(product);

            destinationObject.FarmerId = farmerid;

            int? count = product.CategoryId;
            _adminService.GetCategory((int)count);

            //destinationObject.Category = _adminService.GetCategory((int)count);


            destinationObject.CategoryId = product.CategoryId;

            _adminService.AddProduct(farmerid, destinationObject);
            return Ok("Product Added Successfully");
        }

        [HttpPost("{productid}/image")]
        public async Task<ActionResult<string>> UploadImage(int productid, IFormFile imgFile)
        {
            
                
                _adminService.SaveImage(productid,imgFile);
            

            return Ok("Image Uploaded Successfully");
        }

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

        [HttpGet("removefarmer/{farmerid}")]
        public ActionResult<string> DeleteFarmer(int farmerid)
        {
            _adminService.RemoveFarmer(farmerid);
            return Ok("Farmer Removed Successfully");
        }

        [HttpGet("removeproduct/{productid}")]
        public ActionResult<string> DeleteProduct(int productid)
        {
            _adminService.RemoveProduct(productid);
            return Ok("Product Removed Successfully");
        }

        [HttpPut("updateproduct/{productid}")]
        public ActionResult<string> UpdateProduct(int productid, [FromForm] StockDetail product)
        {
            try
            {
                _adminService.UpdateProduct(product);
                return Ok("Product Updated");
            }
            catch (Exception ex)
            {
                return NotFound($"Product with ID {productid} not found. {ex.Message}");
            }
        }

        // Implement other methods similarly...

        [HttpGet("categorylist")]
        public ActionResult<IEnumerable<Category>> GetCategoryList()
        {
            var categories = _adminService.GetAllCategory();
            return Ok(categories);
        }

        [HttpGet("allorders")]
        public ActionResult<IEnumerable<OrderDetail>> GetAllOrders()
        {
            var orders = _adminService.GetAllOrders();
            return Ok(orders);
        }

        [HttpGet("allusers")]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
       
            List<User> users = _adminService.GetAllUser();

            var destinationObject = _mapper.Map<List <UserDTO> >(users);

            return Ok(users);
        }

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

        [HttpGet("getcategory/{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {
                //Category destinationObject = _mapper.Map<Category>(categoryModel);

                // Validate the incoming data (categoryModel) as needed

                // Call the service method to add the category
                

                // Return a successful response
                return Ok(_adminService.GetCategoryById(categoryId) );
            }
            catch (Exception ex)
            {
                // Handle exceptions and return an error response
                return StatusCode(500, $"Error adding category: {ex.Message}");
            }
        }


        // Implement other methods similarly...
    }
}
