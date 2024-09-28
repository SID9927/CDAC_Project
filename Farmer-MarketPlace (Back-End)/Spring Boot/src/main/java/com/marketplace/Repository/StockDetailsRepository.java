package com.marketplace.Repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.marketplace.Entity.StockDetails;

@Repository
public interface StockDetailsRepository extends JpaRepository<StockDetails, String>{
	public StockDetails findByStockItem(String name);	
}
