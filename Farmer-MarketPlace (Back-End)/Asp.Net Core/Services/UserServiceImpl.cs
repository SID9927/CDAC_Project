using DotnetBackend.Dao;
using DotnetBackend.Data;
using DotnetBackend.Models;

namespace DotnetBackend.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly FarmersmarketContext _dbContext;

        public UserServiceImpl(IUserDao userDao, FarmersmarketContext dbContext)
        {
            _userDao = userDao;
            _dbContext = dbContext;
        }

        public User Authenticate(string email, string password)
        {
            // Implement authentication logic here
            return _userDao.AuthenticateUser(email, password);

        }

        public bool Register(User user)
        {
            // Implement user registration logic here
            return _userDao.RegisterUser(user);
        }

        public CartItem AddToCart(int productId, int qty)
        {
            // Implement adding to cart logic here
            return _userDao.AddToCart(productId, qty);
        }

        public bool PlaceOrder(Cart cart, User user)
        {
            // Implement place order logic here
            return _userDao.PlaceOrder(cart, user);
        }

        public User GetUserDetails(int userId)
        {
            // Implement getting user details logic here
            return _userDao.GetUserDetails(userId);
        }

        public List<OrderDetail> GetOrder(int userId)
        {
            // Implement getting order details logic here
            return _userDao.GetOrders(userId);
        }
    }
}