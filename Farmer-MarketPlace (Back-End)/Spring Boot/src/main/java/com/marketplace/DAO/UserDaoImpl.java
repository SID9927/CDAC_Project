package com.marketplace.DAO;

import java.sql.Date;
import java.util.Calendar;
import java.util.List;

import com.marketplace.Entity.*;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;


import org.springframework.stereotype.Repository;


@Repository
public class UserDaoImpl implements IUserDao {

	@PersistenceContext
	private EntityManager mgr;

	@Override
	public boolean RegisterUser(User user) {
		boolean success = false;
		// Ensure isAdmin is set to a proper boolean value
//		user.setIsAdmin(user.getIsAdmin());
		mgr.persist(user);
		success = true;
		return success;
	}

	@Override
	public void updateUser(User user) {
		mgr.merge(user);
	}

	@Override
	public void removeCategory(int categoryId) {
		Category category = mgr.find(Category.class, categoryId);
		if (category != null) {
			mgr.remove(category);
		}
	}


	@Override
	public User AuthenticateUser(String email, String password) {
		String jpql = "SELECT u FROM User u WHERE email=:em and password=:pass";
		User user = mgr.createQuery(jpql, User.class).setParameter("em", email)
				.setParameter("pass", password).getSingleResult();
		if (user != null)
			return user;
		return null;
	}

	@Override
	public CartItem AddToCart(int productid, int qty) {
		String jpql = "SELECT NEW com.marketplace.Entity.StockDetails(sd.id, sd.stockItem, sd.pricePerUnit) FROM StockDetails sd WHERE sd.id=:Id";
		String jpql2 = "SELECT sd.farmer1 FROM StockDetails sd JOIN sd.farmer1 f WHERE sd.id=:pid";
		StockDetails product = mgr.createQuery(jpql, StockDetails.class)
				.setParameter("Id", productid).getSingleResult();
		Farmer fid = mgr.createQuery(jpql2, Farmer.class).setParameter("pid", productid)
				.getSingleResult();

		CartItem item = new CartItem();
		item.setId(product.getId());
		item.setItem(product.getStockItem());
		item.setPrice(product.getPricePerUnit());
		item.setQty(qty);
		item.setAmount(qty * product.getPricePerUnit());
		item.setFarmer_id(fid.getFarmerId());

		return item;
	}
		@Override
		public boolean PlaceOrder(Cart cart, User user) {
			// Ensure the user is managed by the current persistence context
			user = mgr.merge(user);

			Orders order = new Orders();
			order.setUser(user);  // This will use the existing user, not create a new one
			order.setDeliveryStatus(false);
			order.setPaymentStatus(true);
			order.setPlaceOrderDate(new java.sql.Date(System.currentTimeMillis()));
			order.setDeliveryDate(addDays(new java.sql.Date(System.currentTimeMillis()), 3));

			mgr.persist(order);

			for (CartItem item : cart.getItems()) {
				OrderDetails details = new OrderDetails();
				details.setAmount(item.getAmount());
				details.setOrderItem(item.getItem());
				details.setQuantity(item.getQty());

				// Fetch the farmer from the database
				Farmer farmer = mgr.find(Farmer.class, item.getFarmer_id());
				details.setFarmer(farmer);

				details.setOrders(order);

				// Persist each OrderDetails
				mgr.persist(details);

				// Add to the order's list of details
				order.getOrderDetails().add(details);

				// Update stock quantity
				updateStockQuantity(item.getId(), item.getQty());
			}

			// Clear the cart after successful order placement
			cart.getItems().clear();
			cart.setGrandTotal(0);

			return true;
		}
	private void updateStockQuantity(int productId, int orderedQuantity) {
		StockDetails stock = mgr.find(StockDetails.class, productId);
		if (stock != null) {
			int newQuantity = stock.getQuantity() - orderedQuantity;
			if (newQuantity >= 0) {
				stock.setQuantity(newQuantity);
			} else {
				throw new IllegalStateException("Not enough stock for product: " + stock.getStockItem());
			}
		}
	}


	public Date addDays(Date date, int days) {
	        Calendar c = Calendar.getInstance();
	        c.setTime(date);
	        c.add(Calendar.DATE, days);
	        return new Date(c.getTimeInMillis());
	    }

	@Override
	public List<OrderDetails> getOrder(int userId) {
		String jpql = "SELECT NEW com.marketplace.Entity.OrderDetails(o.id, o.orderItem, o.quantity, o.amount, o.orders) FROM OrderDetails o "
				+ "JOIN o.orders ord JOIN ord.user u WHERE u.userId =:userid";
		List<OrderDetails> results = mgr.createQuery(jpql, OrderDetails.class).setParameter("userid", userId).getResultList();
		System.out.println("Orders retrieved for user " + userId + ": " + results.size());
		return results;
	}

	@Override
	public User getUserDetails(int userId) {
		return mgr.find(User.class, userId);
	}


}
