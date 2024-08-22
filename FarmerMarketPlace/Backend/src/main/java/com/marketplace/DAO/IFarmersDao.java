package com.marketplace.DAO;

import java.util.List;

import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;

public interface IFarmersDao {
	
	List<StockDetails> getAllProduct();
	List<Farmer> getAllFarmers();
	Farmer getFarmerDetails(int id);
	List<StockDetails> getFarmerStock(int farmerid);
	StockDetails getProductDetails(int farmerid, int productid);

}
