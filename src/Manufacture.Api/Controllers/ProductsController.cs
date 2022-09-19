using Manufacture.Api.Data;
using Manufacture.Api.Data.Entities;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Manufacture.Api.services.Interfaces;
using Manufacture.Api.services.Repositories;
using Commons;

namespace Manufacture.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{
    private readonly IProduct product;
      private readonly IPublishEndpoint _publishEndpoint;
    public ProductsController(IProduct product ,IPublishEndpoint publishEndpoint 
    )
    {
        this.product=product;
        _publishEndpoint=publishEndpoint;   
    }

    [HttpGet]
    public HttpResult GetAsync()
    {
        var products = product.Get();
        return products;
    }
    [HttpGet]
    public  HttpResult Test()
    {
        _publishEndpoint.Publish<HttpResult>(new HttpResult
        {
            messageCode = MessageCode.Success,
            message = "success",
            content = "test"
        });
    
        return new HttpResult(MessageCode.Error,"test","test");
    }
  
   
}