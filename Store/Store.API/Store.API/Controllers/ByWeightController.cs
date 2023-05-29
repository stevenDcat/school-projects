using Microsoft.AspNetCore.Mvc;
using ProductLibrary.Models;
using Store.API.FakeData;

namespace Store.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ByWeightController : ControllerBase
{
    private readonly ILogger<ByWeightController> _logger;
    public ByWeightController(ILogger<ByWeightController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<ProductByWeight> Get()
    {
        return Filebase.Current.InventoryByWeight;
    }
    
    [HttpPost("AddOrUpdate")]
    public ProductByWeight AddOrUpdate(ProductByWeight product)
    {
        return Filebase.Current.AddOrUpdateInv(product) as ProductByWeight;
    }
    
    [HttpGet("DeleteInventory/{id}")]
    public int DeleteInventory(int id)
    {
        return Filebase.Current.DeleteInventory(id);
    }
    
    [HttpPost("AddCart")]
    public ProductByWeight AddCart(ProductByWeight product)
    {
        return Filebase.Current.AddCart(product) as ProductByWeight;
    }

    [HttpGet("DeleteCart/{id}")]
    public int DeleteCart(int id)
    {
        
        return Filebase.Current.DeleteCart(id);
    }
}