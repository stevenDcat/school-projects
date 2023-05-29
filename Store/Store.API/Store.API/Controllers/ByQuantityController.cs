using Microsoft.AspNetCore.Mvc;
using ProductLibrary.Models;
using Store.API.FakeData;

namespace Store.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ByQuantityController : ControllerBase
{
    private readonly ILogger<ByQuantityController> _logger;
    public ByQuantityController(ILogger<ByQuantityController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<ProductByQuantity> Get()
    {
        return Filebase.Current.InventoryByQuantity;
    }
    
    [HttpPost("AddOrUpdate")]
    public ProductByQuantity AddOrUpdate(ProductByQuantity product)
    {

        return Filebase.Current.AddOrUpdateInv(product) as ProductByQuantity;
    }
    
    [HttpGet("DeleteInventory/{id}")]
    public int DeleteInventory(int id)
    {
        return Filebase.Current.DeleteInventory(id);
    }
    
    [HttpPost("AddCart")]
    public ProductByQuantity AddCart(ProductByQuantity product)
    {
        return Filebase.Current.AddCart(product) as ProductByQuantity;
    }
    
    [HttpGet("DeleteCart/{id}")]
    public int DeleteCart(int id)
    {
        return Filebase.Current.DeleteCart(id);
    }
    
    

}