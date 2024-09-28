using DotnetBackend.Data;
using DotnetBackend.Models;

namespace DotnetBackend.Dao
{
    public class UserDaoImpl : IUserDao
    {
        private readonly FarmersmarketContext _dbContext;

        public UserDaoImpl(FarmersmarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RegisterUser(User user)
        {
            _dbContext.Users.Add(user);
            int count = _dbContext.SaveChanges();
            return count > 0;
        }

        public User AuthenticateUser(string email, string password)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password)?? new User();
        }

        public CartItem AddToCart(int productId, int qty)
        {
            var product = _dbContext.StockDetail
                .Where(sd => sd.ProductId == productId)
                .Select(sd => new CartItem
                {
                    Id = sd.ProductId,
                    Item = sd.StockItem,
                    Price = sd.PricePerUnit,
                    Qty = qty,
                    Amount = qty * sd.PricePerUnit,
                    Farmer_id = sd.FarmerId
                })
                .FirstOrDefault();

            return product ?? new CartItem();
        }

        public bool PlaceOrder(Cart cart, User user)
        {
            Order order = new Order();
            List<CartItem> items = cart.Items;
            _dbContext.SaveChanges();

            return true;
        }

        public User GetUserDetails(int userId)
        {
            return _dbContext.Users
                .Where(u => u.UserId == userId)
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Firstname = u.Firstname,
                    Lastname = u.Lastname,
                    Email = u.Email,
                    PhoneNo = u.PhoneNo,
                    Address = u.Address,
                    Password = u.Password
                })
            .FirstOrDefault() ?? new User();
        }

        public List<OrderDetail> GetOrders(int userId)
        {
            return _dbContext.OrderDetails
                .Where(od => od.Order.User.UserId == userId)
                .Select(od => new OrderDetail
                {
                    Id = od.Id,
                    OrderItem = od.OrderItem,
                    Quantity = od.Quantity,
                    Amount = od.Amount,
                    Order = od.Order
                })
                .ToList();
        }
    }
}