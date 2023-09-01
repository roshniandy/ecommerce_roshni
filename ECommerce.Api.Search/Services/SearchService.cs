using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrderService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            //await Task.Delay(1);
            //return (true, new { Message = "Hello" });
            var ordersResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customersResult = await customersService.GetCustomersAsync(customerId);
            if(ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess ? 
                            productsResult.Products.FirstOrDefault(p => p.Id == item.productId)?.Name : 
                            "Product Info not available";
                        
                    }
                }
                var result = new
                {
                    Customers = customersResult.IsSuccess? customersResult.Customer : new {Name ="Customer Information not available"},
                    Orders = ordersResult.Orders,
                };
                return (true, result);

            }
            return (false, null);
        }
    }
}
