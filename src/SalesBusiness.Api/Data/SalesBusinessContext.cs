using Microsoft.EntityFrameworkCore;
using SalesBusiness.Api.Data.Entities;

namespace SalesBusiness.Api.Data;
public class SalesBusinessContext: DbContext
{
    public SalesBusinessContext(DbContextOptions<SalesBusinessContext> options):base(options)
    {
        
    }    
     public DbSet<Categories> Categories { get; set; }
}