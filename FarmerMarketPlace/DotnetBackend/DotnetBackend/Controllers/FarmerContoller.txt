using DotnetBackend.Models;
using DotnetBackend.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DotnetBackend.Models; // Replace YourNamespace.Models with the actual namespace for your models
using DotnetBackend.Services;
using AutoMapper; // Replace YourNamespace.Services with the actual namespace for your services

[ApiController]
[Route("farmer")]
[Produces("application/json")]
[EnableCors("AllowMultipleOrigins")]
public class FarmerController : ControllerBase
{
    private readonly IFarmersService _fService;
    private readonly IMapper _mapper;
    public FarmerController(IFarmersService fService, IMapper mapper)
    {
        _fService = fService;
        _mapper = mapper;
    }

    [HttpGet("list")]
    public ActionResult<IEnumerable<Farmer>> FarmersList()
    {
        System.Console.WriteLine("in getFarmersList");
        var list = _fService.GetFarmersList();
        return Ok(list);
    }

    [HttpGet("farmerdetails/{farmerid}")]
    public ActionResult<Farmer> GetFarmerDetails(int farmerid)
    {
        var farmerDetails = _fService.GetFarmerDetails(farmerid);
        return Ok(farmerDetails);
    }

    [HttpGet("products/{farmerid}")]
    public ActionResult<IEnumerable<StockDetail>> StockDetails(int farmerid)
    {
        var products = _fService.GetFarmerStock(farmerid);
        return Ok(products);
    }

    [HttpGet("products/{farmerid}/{productid}")]
    public ActionResult<StockDetail> ProductDetails(int farmerid, int productid)
    {
        var product = _fService.GetProductDetails(farmerid, productid);
        return Ok(product);
    }

    [HttpGet("allproducts")]
    public ActionResult<IEnumerable<StockDetail>> ProductList()
    {
        System.Console.WriteLine("in productlist");
        var list = _fService.GetAllProduct();

        //List<StockDetailDTO> destinationObject = _mapper.Map<List<StockDetailDTO>>(list);

        return Ok(list);
    }
}
