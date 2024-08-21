package com.marketplace.Service;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.multipart.MultipartFile;

import com.marketplace.CustomExceptions.ResourceNotFoundException;
import com.marketplace.DAO.IAdminDao;
import com.marketplace.Entity.Category;
import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.OrderDetails;
import com.marketplace.Entity.StockDetails;
import com.marketplace.Entity.User;
import com.marketplace.Repository.StockDetailsRepository;


@Service
@Transactional
public class AdminServiceImpl implements IAdminService {
	
	@Autowired
	private IAdminDao a_dao;
	
	@Autowired
	private StockDetailsRepository stockRepository;

	@Override
	public boolean AddFarmer(Farmer farmer) {
		return a_dao.AddFarmer(farmer);
	}

	@Override
	public boolean AddProduct(int farmerid, StockDetails product) {
		return a_dao.AddProduct(farmerid, product);
	}
	
	@Override
	public boolean RemoveFarmer(int farmerid) {
		return a_dao.RemoveFarmer(farmerid);
	}

	@Override
	public boolean RemoveProduct(int productid) {
		return a_dao.RemoveProduct(productid);
	}

	@Override
	public boolean UpdateProduct(StockDetails product) {
		return a_dao.UpdateProduct(product);
	}

	@Override
	public boolean UpdateFarmer(Farmer farmer) {
		return a_dao.UpdateFarmer(farmer);
	}

	@Override
	public Farmer GetFarmerDetails(int farmerid) {
		return a_dao.GetFarmerDetails(farmerid);
	}

	@Override
	public StockDetails GetProductDetails(int productid) {
		return a_dao.GetProductDetails(productid);
	}

	@Override
	public Category GetCategory(int catid) {
		return a_dao.GetCategory(catid);
	}

	@Override
	public boolean SetCategory(String category) {
		return a_dao.SetCategory(category);
	}

	@Override
	public boolean RemoveCategory(int catid) {
		return a_dao.RemoveCategory(catid);
	}

	@Override
	public String saveImage(int productId, MultipartFile imgFile) throws IOException {
		return a_dao.saveImage(productId, imgFile);
	}

	@Override
	public byte[] restoreImage(int empId) throws IOException {
		return a_dao.restoreImage(empId);
	}
	
	@Override
	public List<Category> getAllCategory(){
		return a_dao.getAllCategory();
	}
	
	@Override
	public List<OrderDetails> getAllOrders() {
		return a_dao.getAllOrders();
	}

	@Override
	public List<User> getAllUser() {
		// TODO Auto-generated method stub
		return a_dao.getAllUser();
	}

	@Override
	public boolean UpdateUser(User user) {
		return a_dao.UpdateUser(user);
	}

	public byte[] restoreImageAgain(String productName) throws IOException {
		StockDetails s = stockRepository.findByStockItem(productName);
		String path = s.getImagePath();
		if (path != null)
			return Files.readAllBytes(Paths.get(path));
		throw new ResourceNotFoundException("Image not  yet assigned , for " + s.getStockItem());
	}

}
