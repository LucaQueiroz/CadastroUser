using MySQLcrudBlazor.Server;
using Microsoft.AspNetCore.Mvc;
using MySQLcrudBlazor.Shared;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    private readonly AppDbContext db;
    
    public ProductController(AppDbContext db)
    {
        this.db = db;
    }

[HttpGet]
[Route("List")]
public async Task<IActionResult> Get()
    {
        var products = await db.Products.ToListAsync();
        return Ok(products);
    }

public async Task<ActionResult> Post([FromBody] Product product)
    {
        try
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };

            db.Add(newProduct);
            await db.SaveChangesAsync();
            return Ok();
        }
        catch(Exception e)
        {
            return View(e);
        }

        
    }
}