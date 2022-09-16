using Commons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.Data.Entities;
using SalesBusiness.Api.services.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace SalesBusiness.Api.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrdersController : ControllerBase
{
    private ICategories context;
    public OrdersController(ICategories context)
    {
        this.context = context;
    }    
    [HttpGet]
    public HttpResult Get()
    {
        return context.Get();
    }
    [HttpPost]
    public HttpResult Post(Categories c)
    {
        return context.Post(c);
    }
}