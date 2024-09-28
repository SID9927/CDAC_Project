package com.marketplace.DAO;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.StandardCopyOption;
import java.util.List;
import jakarta.persistence.EntityManager;
import jakarta.persistence.PersistenceContext;
import org.springframework.stereotype.Repository;
import org.springframework.web.multipart.MultipartFile;

import com.marketplace.CustomExceptions.ResourceNotFoundException;
import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;


@Repository
public class FarmersDaoImpl implements IFarmersDao {

	@PersistenceContext
	private EntityManager mgr;
	
	@Override
	public List<Farmer> getAllFarmers() {
		String jpql = "SELECT NEW com.marketplace.Entity.Farmer(f.farmerId, f.firstname, f.lastname, f.email, f.phoneNo, f.address) FROM Farmer f";
		return mgr.createQuery(jpql, Farmer.class).getResultList();
	}

	@Override
	public List<StockDetails> getFarmerStock(int farmerid) {
		String jpql = "SELECT NEW com.marketplace.Entity.StockDetails(sd.id, sd.stockItem, sd.pricePerUnit) FROM StockDetails sd JOIN sd.farmer1 f WHERE f.farmerId=:frmr";
		return mgr.createQuery(jpql, StockDetails.class).setParameter("frmr", farmerid).getResultList();
	}

	@Override
	public StockDetails getProductDetails(int farmerid, int productid) {
		String jpql = "SELECT NEW com.marketplace.Entity.StockDetails(sd.id, sd.stockItem, sd.quantity, sd.pricePerUnit, sd.category) FROM StockDetails sd JOIN sd.farmer1 f WHERE f.farmerId=:frmr AND sd.id=:prdct";
		return mgr.createQuery(jpql, StockDetails.class).setParameter("frmr", farmerid).setParameter("prdct", productid).getSingleResult();
	}

	@Override
	public Farmer getFarmerDetails(int id) {
		String jpql = "SELECT NEW com.marketplace.Entity.Farmer(f.farmerId, f.firstname, f.lastname, f.email, f.phoneNo, f.address) FROM Farmer f WHERE f.farmerId=:fid";
		return mgr.createQuery(jpql, Farmer.class).setParameter("fid", id).getSingleResult();
	}

	@Override
	public List<StockDetails> getAllProduct() {
		String jpql = "SELECT NEW com.marketplace.Entity.StockDetails(s.id, s.stockItem, s.quantity, s.pricePerUnit, s.category, s.imagePath) FROM StockDetails s";
		return mgr.createQuery(jpql, StockDetails.class).getResultList();
	}
		@Override
		public String saveImage(int productId, MultipartFile imgFile) throws IOException {
			StockDetails s = mgr.find(StockDetails.class, productId);
			s.setImagePath(imgFile.getOriginalFilename());
			s.setImageData(imgFile.getBytes());
			return "Image saved successfully";
		}

		@Override
		public byte[] restoreImage(int productId) throws IOException {
			StockDetails s = mgr.find(StockDetails.class, productId);
			if (s.getImageData() != null)
				return s.getImageData();
			throw new ResourceNotFoundException("Image not yet assigned for product ID " + productId);
		}

		@Override
		public byte[] restoreImageByOrderItem(String orderItem) throws IOException {
			String jpql = "SELECT s FROM StockDetails s WHERE s.stockItem = :orderItem";
			StockDetails s = mgr.createQuery(jpql, StockDetails.class)
					.setParameter("orderItem", orderItem)
					.getSingleResult();
			if (s.getImageData() != null)
				return s.getImageData();
			throw new ResourceNotFoundException("Image not yet assigned for " + orderItem);
		}
}
