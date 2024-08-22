using System;
using System.Collections.Generic;
using DotnetBackend.Dao;
using DotnetBackend.Models;

namespace DotnetBackend.Services
{
    public class UserServiceImpl : IUserService
    {
        // You can inject any required dependencies in the constructor
private readonly IUserDao userDao;
      
        public UserServiceImpl(IUserDao _userDao)
        {
            userDao = _userDao; 
           
        }
        public User Authenticate(string email, string password)
        {
            // Implement authentication logic here
            return userDao.AuthenticateUser(email, password);
            
        }

        public bool Register(User user)
        {
            // Implement user registration logic here
           return userDao.RegisterUser(user);
        }

        public CartItem AddToCart(int productId, int qty)
        {
            // Implement adding to cart logic here
           return userDao.AddToCart(productId, qty);
        }

        public bool PlaceOrder(Cart cart, User user)
        {
            // Implement place order logic here
           return userDao.PlaceOrder(cart, user);   
        }

        public User GetUserDetails(int userId)
        {
            // Implement getting user details logic here
           return userDao.GetUserDetails(userId);
        }

        public List<OrderDetail> GetOrder(int userId)
        {
            // Implement getting order details logic here
          return userDao.GetOrders(userId);
        }
    }
}
