namespace ECommerce.Api.Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int productId { get; set; }
        public int Quantity { get;set; }
        public int UnitPrice { get; set; }  
    }
}
