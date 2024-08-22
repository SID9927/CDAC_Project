using DotnetBackend.Data;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DotnetBackend.Controllers
{
    [ApiController]
    [Route("admin")]
    [EnableCors("AllowMultipleOrigins")]
    public class StatsController : ControllerBase
    {
        private readonly FarmersmarketContext _context;

        public StatsController(FarmersmarketContext context)
        {
            _context = context;
        }

        [HttpGet("farmercount")]
        public async Task<ActionResult<int>> GetFarmerCount()
        {
            return await _context.Farmers.CountAsync();
        }

        [HttpGet("categorycount")]
        public async Task<ActionResult<int>> GetCategoryCount()
        {
            return await _context.Categories.CountAsync();
        }

        [HttpGet("productcount")]
        public async Task<ActionResult<int>> GetProductCount()
        {
            return await _context.StockDetail.CountAsync();
        }

        [HttpGet("ordercount")]
        public async Task<ActionResult<int>> GetOrderCount()
        {
            return await _context.OrderDetails.CountAsync();
        }

        [HttpGet("usercount")]
        public async Task<ActionResult<int>> GetUserCount()
        {
            return await _context.Users.CountAsync();
        }
    }
}
