package com.marketplace.Service;

import java.util.List;
import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;

public interface IFarmersService {
	
	List<Farmer> getFarmersList();
	Farmer getFarmerDetails(int id);
	List<StockDetails> getFarmerStock(int farmerid);
	StockDetails getProductDetails(int farmerid, int productid);
	List<StockDetails> getAllProduct();
	
}
