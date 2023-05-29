using Microsoft.AspNetCore.Mvc;
using ProductLibrary.Models;
using Store.API.FakeData;

namespace Store.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet("Inventory")]
    public List<Product> GetInventory()
    {
        return Filebase.Current.Inventory;
    }
    
    [HttpGet("Cart")]
    public List<Product> GetCart()
    {
        return Filebase.Current.Cart;
    }


    [HttpPost("SaveCart")]
    public string SaveCart(KeyValuePair<string, List<Product>> pair)
    {
        return Filebase.Current.SaveCart(pair);
    }
    
    [HttpPost("LoadCart")]
    public List<Product> LoadCart(KeyValuePair<string, List<Product>> pair)
    {
        return Filebase.Current.LoadCart(pair);

    }

    [HttpGet("SavesList")]
    public List<string> GetSaves()
    {
        return Filebase.Current.SavesList();
    }

    [HttpPost("DeleteSave")]
    public string DeleteSave(KeyValuePair<string, List<Product>> pair)
    {
        return Filebase.Current.DeleteSave(pair);
    }

    [HttpGet("Pay")]
    public string Pay()
    {
        return Filebase.Current.CartClear();
    }


}