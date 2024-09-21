package com.marketplace.Service;

import java.io.IOException;
import java.util.List;

import org.springframework.web.multipart.MultipartFile;

import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;

public interface IFarmersService {
	
	List<Farmer> getFarmersList();
	Farmer getFarmerDetails(int id);
	List<StockDetails> getFarmerStock(int farmerid);
	StockDetails getProductDetails(int farmerid, int productid);
	List<StockDetails> getAllProduct();
	String saveImage(int productId, MultipartFile imgFile) throws IOException;
	byte[] restoreImage(int productId) throws IOException;
	byte[] restoreImageByOrderItem(String orderItem) throws IOException;

}
