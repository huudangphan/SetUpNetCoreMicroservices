using MassTransit;
using Commons;
using SalesBusiness.Api.Data.Entities;
using SalesBusiness.Api.services.Interfaces;
using SalesBusiness.Api.services.Repositories;

namespace SalesBusiness.Api.Consumer
{
    public class TestConsumer : IConsumer<HttpResult>
    {
        private ICategories _context;
        
        public TestConsumer(ICategories context)
        {
            this._context= context;  
        }
        public Task Consume(ConsumeContext<HttpResult> context)
        {
            var entity = new Categories { Id = (int)context.Message.messageCode, Name = context.Message.message, description = context.Message.message };
            _context.Post(entity);
            ExampleAsync(context.Message.message);
            return Task.CompletedTask;

        }
        public static async Task ExampleAsync(string mess)
        {
            string text =
               mess;

            await File.WriteAllTextAsync("WriteText.txt", text);
        }
    }
}
