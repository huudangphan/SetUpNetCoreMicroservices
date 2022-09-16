using SalesBusiness.Api.Data.Entities;
using SalesBusiness.Api.Data;
using SalesBusiness.Api.services.Interfaces;
using Commons;
using System.Diagnostics.CodeAnalysis;

namespace SalesBusiness.Api.services.Repositories
{
    public class CategoryRepository : ICategories
    {
        private SalesBusinessContext context;
        public CategoryRepository(SalesBusinessContext context)
        {
            this.context = context;
        }
        public HttpResult Get()
        {
            try
            {
                var result = context.Categories.ToList();
                return new HttpResult(MessageCode.Success, "Success", result);
            }
            catch (Exception ex)
            {

                return new HttpResult(MessageCode.Error, Functions.ToString(ex.Message));
            }
        }

        public HttpResult Post(Categories categories)
        {
            try
            {
                context.Categories.Add(categories);
                context.SaveChanges();
                return new HttpResult(MessageCode.Success);
            }
            catch (Exception ex)
            {

                return new HttpResult(MessageCode.Error, Functions.ToString(ex.Message));
            }
        }
    }
}
