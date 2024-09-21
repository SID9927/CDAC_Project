package com.marketplace.Service;

import java.util.List;

import com.marketplace.Entity.Cart;
import com.marketplace.Entity.CartItem;
import com.marketplace.Entity.OrderDetails;
import com.marketplace.Entity.User;

public interface IUserService {
	
	public User Authenticate(String email, String password);
	public boolean Register(User user);
	public CartItem AddToCart(int productid, int qty);
	public boolean PlaceOrder(Cart cart, User user);
	User getUserDetails(int userId);
	public List<OrderDetails> getOrder(int userId);
	public void updateUser(User user);
	public void removeCategory(int categoryId);

}
