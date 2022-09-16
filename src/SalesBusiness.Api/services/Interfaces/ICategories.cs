using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons;
using SalesBusiness.Api.Data.Entities;
namespace SalesBusiness.Api.services.Interfaces
{
    public interface ICategories
    {
        HttpResult Get();
        HttpResult Post(Categories categories);
    }
}