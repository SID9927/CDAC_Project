package com.marketplace.DAO;

import java.io.IOException;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;

public interface IFarmersDao {
	
	List<StockDetails> getAllProduct();
	List<Farmer> getAllFarmers();
	Farmer getFarmerDetails(int id);
	List<StockDetails> getFarmerStock(int farmerid);
	StockDetails getProductDetails(int farmerid, int productid);
String saveImage(int productId, MultipartFile imgFile) throws IOException;
	byte[] restoreImageByOrderItem(String orderItem) throws IOException;
}
