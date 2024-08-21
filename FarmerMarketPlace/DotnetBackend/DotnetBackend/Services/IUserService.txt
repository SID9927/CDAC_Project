using System.Collections.Generic;
using DotnetBackend.Models;

namespace DotnetBackend.Services
{
    public interface IUserService
    {
        User Authenticate(string email, string password);
        bool Register(User user);
        CartItem AddToCart(int productId, int qty);
        bool PlaceOrder(Cart cart, User user);
        User GetUserDetails(int userId);
        List<OrderDetail> GetOrder(int userId);
    }
}
