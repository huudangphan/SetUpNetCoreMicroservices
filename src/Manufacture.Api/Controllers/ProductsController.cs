using Manufacture.Api.Data;
using Manufacture.Api.Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Manufacture.Api.services.Interfaces;
using Commons;

namespace Manufacture.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly IProduct product;
  
    public ProductsController(IProduct product  
    )
    {
        this.product=product;        
    }

    [HttpGet]
    public HttpResult GetAsync()
    {
        var products = product.Get();
        return products;
    }
    [HttpGet]
    public HttpResult Test()
    {
        return new HttpResult(MessageCode.Error,"test","test");
    }
    // [HttpGet]
    // [Route("{id}")]
    // public async Task<IActionResult> GetAsync(int id)
    // {
    //     var product = await _manufactureContext.Products.FindAsync(id);
    //     return Ok(product);
    // }   
}