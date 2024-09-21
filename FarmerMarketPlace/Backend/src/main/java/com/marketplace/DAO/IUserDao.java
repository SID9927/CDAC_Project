package com.marketplace.DAO;

import java.util.List;

import com.marketplace.Entity.Cart;
import com.marketplace.Entity.CartItem;
import com.marketplace.Entity.OrderDetails;
import com.marketplace.Entity.User;

public interface IUserDao {
	
	public boolean RegisterUser(User user);
	public User AuthenticateUser(String email, String password);
	public CartItem AddToCart(int productid, int qty);
	public boolean PlaceOrder(Cart cart, User user);
	User getUserDetails(int userId);
	public List<OrderDetails> getOrder(int userId);
	void updateUser(User user);
	void removeCategory(int categoryId);
}
