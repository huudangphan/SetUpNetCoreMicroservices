using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons;
using Manufacture.Api.Data;
using Manufacture.Api.services.Interfaces;

namespace Manufacture.Api.services.Repositories
{
    public class ProductRepository :IProduct
    {
        private ManufactureContext context;
        public ProductRepository(ManufactureContext context){
            this.context=context;
        }
        public HttpResult Get()
        {
            try
            {
                var result=context.Products.ToList();
                 return new HttpResult(
                    MessageCode.Success,
                   "Success",
                   result
                    );
            }
            catch(Exception ex)
            {
                return new HttpResult(MessageCode.Error, Functions.ToString(ex.Message));
            }
        }
    }
}