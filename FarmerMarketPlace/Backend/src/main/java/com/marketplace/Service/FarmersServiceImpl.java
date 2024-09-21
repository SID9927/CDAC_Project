package com.marketplace.Service;

import java.io.IOException;
import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.web.multipart.MultipartFile;

import com.marketplace.DAO.IFarmersDao;
import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;


@Service
@Transactional
public class FarmersServiceImpl implements IFarmersService {

	@Autowired
	private IFarmersDao f_dao;
	
	@Override
	public List<Farmer> getFarmersList() {
		return f_dao.getAllFarmers();
	}

	@Override
	public List<StockDetails> getFarmerStock(int farmerid) {
		return f_dao.getFarmerStock(farmerid);
	}

	@Override
	public StockDetails getProductDetails(int farmerid, int productid) {
		return f_dao.getProductDetails(farmerid, productid);
	}

	@Override
	public Farmer getFarmerDetails(int id) {
		return f_dao.getFarmerDetails(id);
	}

	@Override
	public List<StockDetails> getAllProduct() {
		return f_dao.getAllProduct();
	}
	
	@Override
	public String saveImage(int productId, MultipartFile imgFile) throws IOException {
		return f_dao.saveImage(productId, imgFile);
	}

	@Override
	public byte[] restoreImage(int productId) throws IOException {
		return f_dao.restoreImage(productId);
	}

	@Override
	public byte[] restoreImageByOrderItem(String orderItem) throws IOException {
		return f_dao.restoreImageByOrderItem(orderItem);
	}

}
