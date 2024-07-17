namespace OrderManagementSystem.Application.DTOs
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public ProductDto? Product { get; set; }

    }
}
