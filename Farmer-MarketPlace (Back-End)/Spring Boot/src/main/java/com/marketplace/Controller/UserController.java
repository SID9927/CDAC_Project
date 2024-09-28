/**
 * The UserController class is a Spring REST controller that handles user-related operations for the Farmer Market Place application.
 * It provides endpoints for user registration, login, retrieving user details, updating user information, removing categories,
 * adding products to the cart, checking out the cart, removing items from the cart, placing orders, and retrieving order history.
 *
 * The controller uses the IUserService and PdfExportService interfaces to interact with the application's services, and the
 * EmailSenderService to send email notifications to users.
 */
package com.marketplace.Controller;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URISyntaxException;
import java.util.ArrayList;
import java.util.List;

import jakarta.mail.MessagingException;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.itextpdf.text.DocumentException;
import com.marketplace.Entity.*;
import com.marketplace.Service.EmailSenderService;
import com.marketplace.Service.IUserService;
import com.marketplace.Service.PdfExportService;

@CrossOrigin(origins = {"http://localhost:3000", "https://farmer-market-place.vercel.app"})
@RestController
@RequestMapping("/user")
public class UserController {

	@Autowired
	private IUserService u_service;

	@Autowired
	private PdfExportService pdfService;

	@Autowired
	private EmailSenderService mailService;

	List<CartItem> items = new ArrayList<>();
	Cart mycart = new Cart();
	User user = new User();

	@PostMapping("/register")
	public ResponseEntity<?> RegisterNewUser(@RequestBody User user) {
		u_service.Register(user);
		String subject = "Registration Successful " + user.getFirstname();
		String body = "Congratulations Registration is Successful \n"+ user.getFirstname() + " "+ user.getLastname() +". Welcome to Farmer Market Place";
		mailService.sendSimpleEmail(user.getEmail(),body,subject);
		return new ResponseEntity<>("Registration Successful..!!", HttpStatus.CREATED);
	}

	@PostMapping("/login")
	public ResponseEntity<?> LoginUser(@RequestBody Authentication userID) {
		String email = userID.getEmail();
		String password = userID.getPassword();
		System.out.println(email + "   " + password);
		User u = null;
		try {
			u = u_service.Authenticate(email, password);
		} catch (Exception e) {
			return new ResponseEntity<>(HttpStatus.UNAUTHORIZED);
		}

		user = u;
		String subject = "Login Attempt " + user.getFirstname();
		String body = "Login Attempt at Farmers Market Place "+ user.getFirstname() + " "+ user.getLastname() +". \n If not done by you. Contact SIDDHARTH.";

		mailService.sendSimpleEmail(user.getEmail(),body,subject);
		items = new ArrayList<>();
		return new ResponseEntity<>(u, HttpStatus.OK);
	}

	@GetMapping("/userdetails/{userId}")
	public ResponseEntity<?> GetUserDetails(@PathVariable int userId) {
		User user = u_service.getUserDetails(userId);
		if (user == null) {
			return new ResponseEntity<>(HttpStatus.NOT_FOUND);
		}
		return new ResponseEntity<>(user, HttpStatus.OK);
	}

	@PostMapping("/updateuser/{userId}")
	public ResponseEntity<?> UpdateUser(@PathVariable int userId, @RequestBody User user) {
		User u = u_service.getUserDetails(userId);
		if (u != null) {
			u.setFirstname(user.getFirstname());
			u.setLastname(user.getLastname());
			u.setEmail(user.getEmail());
			u.setAddress(user.getAddress());
			u.setPhoneNo(user.getPhoneNo());
			u.setIsAdmin(user.getIsAdmin());

			u_service.updateUser(u);
			return new ResponseEntity<>(u, HttpStatus.OK);
		}
		return new ResponseEntity<>(HttpStatus.NOT_FOUND);
	}

	@DeleteMapping("/removecategory/{catid}")
	public ResponseEntity<?> RemoveCategory(@PathVariable int catid) {
		u_service.removeCategory(catid);
		return new ResponseEntity<>("Category Removed", HttpStatus.OK);
	}

	@PostMapping("/addtocart/{productid}")
	public ResponseEntity<?> AddToCart(@PathVariable int productid, @RequestParam int qty) {
		CartItem product = u_service.AddToCart(productid, qty);
		items.add(product);
		return new ResponseEntity<>(items, HttpStatus.OK);
	}

	@GetMapping("/checkout")
	public ResponseEntity<?> CheckOut() {
		double grandtotal = 0.0;
		if (items != null && !items.isEmpty()) {
			for (CartItem item : items) {
				grandtotal += item.getAmount();
			}
			mycart.setItems(items);
			mycart.setGrandTotal(grandtotal);
			return new ResponseEntity<>(items, HttpStatus.OK);
		} else {
			return new ResponseEntity<>("Cart is empty.", HttpStatus.OK);
		}
	}

	@PostMapping("/removefromcart/{productid}")
	public ResponseEntity<?> removeItem(@PathVariable int productid) {
		if (productid < 0 || productid >= items.size()) {
			return new ResponseEntity<>("Index out of bounds", HttpStatus.BAD_REQUEST);
		}
		items.remove(productid);
		return new ResponseEntity<>(items, HttpStatus.OK);
	}

	@PostMapping("/placeorder")
	public ResponseEntity<?> PlaceOrder() throws DocumentException, MessagingException, MalformedURLException, URISyntaxException, IOException {
		u_service.PlaceOrder(mycart, user);
		user = u_service.getUserDetails(user.getUserId());
		pdfService.export(items);

		if (user != null && user.getEmail() != null && !user.getEmail().isEmpty()) {
			mailService.sendEmailWithAttachment(user.getEmail(),
					"Please check below attached pdf for details. Have a good day!",
					"Your order is placed.",
					"receipt.pdf");
		} else {
			System.out.println("Warning: Unable to send email. User email is null or empty.");
		}

		items.clear();
		return new ResponseEntity<>(items, HttpStatus.OK);
	}

	@PostMapping("/orders")
	public ResponseEntity<?> Orders(@RequestParam int userId) {
		List<OrderDetails> orders = u_service.getOrder(userId);
		return new ResponseEntity<>(orders, HttpStatus.OK);
	}
}
