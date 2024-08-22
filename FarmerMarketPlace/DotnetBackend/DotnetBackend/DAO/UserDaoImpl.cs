using System;
using System.Collections.Generic;
using System.Linq;
using DotnetBackend.Dao;
using DotnetBackend.Models;
using DotnetBackend.Dao;
using Microsoft.EntityFrameworkCore;
using DotnetBackend.Data;

namespace DotnetBackend.Dao
{
    public class UserDaoImpl : IUserDao
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly FarmersmarketContext _dbContect;
		public UserDaoImpl(IDbContextFactory dbContextFactory, FarmersmarketContext dbContect)
        {
            _dbContextFactory = dbContextFactory;
            this._dbContect = dbContect;
        }


        public bool RegisterUser(User user)
        {
            using (var dbContext = _dbContextFactory.CreateDbContext())
            {
                dbContext.Users.Add(user);
                int count = dbContext.SaveChanges();
                return count > 0;
            }
        }


        public User AuthenticateUser(string email, string password)
        {
            return _dbContect.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public CartItem AddToCart(int productId, int qty)
        {
            var product = _dbContextFactory.CreateDbContext().StockDetail
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

            return product;
        }

        public bool PlaceOrder(Cart cart, User user)
        {
            Order order = new Order();
            List<CartItem> items = cart.Items;
            _dbContextFactory.CreateDbContext().SaveChanges();

            return true;
        }

        public User GetUserDetails(int userId)
        {
            return _dbContextFactory.CreateDbContext().Users
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
            .FirstOrDefault();
        }

        public List<OrderDetail> GetOrders(int userId)
        {
            return _dbContextFactory.CreateDbContext().OrderDetails
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
