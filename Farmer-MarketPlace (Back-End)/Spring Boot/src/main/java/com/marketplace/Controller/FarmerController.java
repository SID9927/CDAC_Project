/**
 * The FarmerController class provides REST API endpoints for managing farmer-related data in the marketplace application.
 * It handles requests related to fetching farmer lists, farmer details, product details, and uploading/downloading product images.
 */
package com.marketplace.Controller;

import java.io.IOException;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.marketplace.Entity.Farmer;
import com.marketplace.Entity.StockDetails;
import com.marketplace.Service.IFarmersService;

@CrossOrigin(origins = {"http://localhost:3000", "https://farmer-market-place.vercel.app"})
@RestController
@RequestMapping("/farmer")
public class FarmerController {

	@Autowired
	private IFarmersService f_service;

	@GetMapping("/list")
	public ResponseEntity<?> farmersList() {
		System.out.println("in getFarmersList");
		List<Farmer> list = f_service.getFarmersList();
		return new ResponseEntity<List<Farmer>>(list, HttpStatus.OK);
	}

	@GetMapping("/farmerdetails/{farmerid}")
	public ResponseEntity<?> getFarmerDetails(@PathVariable int farmerid) {
		return new ResponseEntity<Farmer>(f_service.getFarmerDetails(farmerid), HttpStatus.OK);
	}

	@GetMapping("/products/{farmerid}")
	public ResponseEntity<?> stockDetails(@PathVariable int farmerid) {
		List<StockDetails> products = f_service.getFarmerStock(farmerid);
		return new ResponseEntity<List<StockDetails>>(products, HttpStatus.OK);
	}

	@GetMapping("/products/{farmerid}/{productid}")
	public ResponseEntity<?> productDetails(@PathVariable int farmerid, @PathVariable int productid) {
		StockDetails product = f_service.getProductDetails(farmerid, productid);
		return new ResponseEntity<StockDetails>(product, HttpStatus.OK);
	}
	
	@GetMapping("/allproducts")
	public ResponseEntity<?> productlist() {
		System.out.println("in productlist");
		List<StockDetails> list = f_service.getAllProduct();
		return new ResponseEntity<List<StockDetails>>(list, HttpStatus.OK);
	}

	@PostMapping(value = "/{productid}/image", consumes = MediaType.MULTIPART_FORM_DATA_VALUE)
	public ResponseEntity<?> uploadImage(@PathVariable int productid, @RequestParam("imgFile") MultipartFile imgFile)
			throws IOException {
		if (imgFile.getSize() > 5_000_000) { // 5MB limit
			return ResponseEntity.badRequest().body("File is too large. Maximum size is 5MB.");
		}
		String msg = f_service.saveImage(productid, imgFile);
		return ResponseEntity.ok().body(msg);
	}


	@GetMapping(value = "/{identifier}/image", produces = { MediaType.IMAGE_GIF_VALUE, MediaType.IMAGE_JPEG_VALUE,
			MediaType.IMAGE_PNG_VALUE })
	public ResponseEntity<?> downloadImage(@PathVariable String identifier) throws IOException {
		byte[] imageData;
		try {
			int productId = Integer.parseInt(identifier);
			imageData = f_service.restoreImage(productId);
		} catch (NumberFormatException e) {
			// If identifier is not a number, treat it as an orderItem
			imageData = f_service.restoreImageByOrderItem(identifier);
		}
		return ResponseEntity.ok().contentType(MediaType.IMAGE_JPEG).body(imageData);
	}

}
