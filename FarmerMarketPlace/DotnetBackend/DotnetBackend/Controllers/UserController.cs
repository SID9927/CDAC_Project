using DotnetBackend.Models;
using DotnetBackend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using DotnetBackend.Services;
using AutoMapper;
using DotnetBackend.DTO; // Replace YourNamespace.Services with the actual namespace for your services

[ApiController]
[Route("user")]
[Produces("application/json")]

[EnableCors("AllowMultipleOrigins")]
public class UserController : ControllerBase
{
    private readonly IUserService _uService;
    //private readonly PdfExportService _pdfService;
    //private readonly EmailSenderService _mailService;
    private readonly IMapper _mapper;

    private List<CartItem> _items;
    private Cart _myCart;
    private User _user;

    public UserController(IUserService uService, IMapper mapper)
    {
        _uService = uService;
        _mapper = mapper;
        //_pdfService = pdfService;
        //_mailService = mailService;
        _items = new List<CartItem>();
        _myCart = new Cart();
        _user = new User();
    }

    [HttpPost("register")]
    public ActionResult<string> RegisterNewUser([FromBody] UserDTO user)
    {
        User destinationObject = _mapper.Map<User>(user);

        _uService.Register(destinationObject);
        string subject = $"Registration Successful {user.Firstname}";
        string body = $"Congratulations Registration is Successful\n{user.Firstname} {user.Lastname}. Welcome to Farmer Market Place";
        //_mailService.SendSimpleEmailAsync(user.Email, body, subject);
        return Created("Registration Successful..!!", destinationObject);
    }

    [HttpPost("login")]
    public ActionResult<User> LoginUser([FromBody] Authentication userID)
    {
        string email = userID.Email;
        string password = userID.Password;
        Console.WriteLine($"{email}   {password}");
        User u = null;
        try
        {
            u = _uService.Authenticate(email, password);
        }
        catch (Exception e)
        {
            return Ok();
        }

        _user = u;
        string subject = $"Login Attempt {_user.Firstname}";
        string body = $"Login Attempt at Farmers Market Place {_user.Firstname} {_user.Lastname}.\n If not done by you. Contact Jitya Patil from Sakri";
        //_mailService.SendSimpleEmailAsync(_user.Email, body, subject);
        _items = new List<CartItem>();
        return Ok(u);
    }

    [HttpPost("addtocart/{productid}")]
    public ActionResult<List<CartItem>> AddToCart(int productid, [FromQuery] int qty)
    {
        Console.WriteLine("in AddToCart");
        CartItem product = _uService.AddToCart(productid, qty);
        _items.Add(product);
        Console.WriteLine("item added to cart");
        Console.WriteLine(_items);
        return Ok(_items);
    }

    [HttpGet("checkout")]
    public ActionResult<List<CartItem>> CheckOut()
    {
        Console.WriteLine("checkout");
        double? grandtotal = 0.0;

        foreach (CartItem item in _items)
        {
            grandtotal += item.Amount;
        }
        _myCart.Items = _items;
        _myCart.GrandTotal = grandtotal;
        return Ok(_items);
    }

    [HttpPost("removefromcart/{productid}")]
    public ActionResult<List<CartItem>> RemoveItem(int productid)
    {
        Console.WriteLine("Removing item");
        _items.RemoveAt(productid);
        return Ok(_items);
    }

    [HttpPost("placeorder")]
    public async Task<ActionResult<List<CartItem>>> PlaceOrder()
    {
        _uService.PlaceOrder(_myCart, _user);

        //_pdfService.Export(_items);

        //_mailService.SendEmailWithAttachmentAsync(_user.Email,
        //    "Please check below attached pdf for details. Have a good day!", "Your order is placed.",
        //    "receipt.pdf");
        _items.Clear();
        return Ok(_items);
    }

    [HttpPost("orders")]
    public ActionResult<List<OrderDetail>> Orders([FromQuery] int userId)
    {
        Console.WriteLine($"inside orders {userId}");
        List<OrderDetail> orders = _uService.GetOrder(userId);
        return Ok(orders);
    }
}
