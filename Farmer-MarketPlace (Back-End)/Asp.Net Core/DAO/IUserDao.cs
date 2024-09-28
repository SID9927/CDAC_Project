using DotnetBackend.Models;

namespace DotnetBackend.Dao
{
    public interface IUserDao
    {
        bool RegisterUser(User user);
        User AuthenticateUser(string email, string password);
        CartItem AddToCart(int productId, int qty);
        bool PlaceOrder(Cart cart, User user);
        User GetUserDetails(int userId);
        List<OrderDetail> GetOrders(int userId);
    }
}
