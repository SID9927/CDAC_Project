package com.marketplace.Service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import com.marketplace.DAO.IUserDao;
import com.marketplace.Entity.Cart;
import com.marketplace.Entity.CartItem;
import com.marketplace.Entity.OrderDetails;
import com.marketplace.Entity.User;

@Service
@Transactional
public class UserServiceImpl implements IUserService {

	@Autowired
	private IUserDao u_dao;

	@Override
	public boolean Register(User user) {
		u_dao.RegisterUser(user);
		return false;
	}

	@Override
	public void updateUser(User user) {
		u_dao.updateUser(user);
	}

	@Override
	public void removeCategory(int categoryId) {
		u_dao.removeCategory(categoryId);
	}


	@Override
	public User Authenticate(String email, String password) {
		User user = u_dao.AuthenticateUser(email, password);
		return user;
	}

	@Override
	public CartItem AddToCart(int productid, int qty) {
		return u_dao.AddToCart(productid, qty);
	}

		@Override
		@Transactional
		public boolean PlaceOrder(Cart cart, User user) {
			try {
				return u_dao.PlaceOrder(cart, user);
			} catch (Exception e) {
				e.printStackTrace();
				return false;
			}
		}

	@Override
	@Transactional(readOnly = true)
	public User getUserDetails(int userId) {
		return u_dao.getUserDetails(userId);
	}


	@Override
	public List<OrderDetails> getOrder(int userId) {
		return u_dao.getOrder(userId);
	}
}
