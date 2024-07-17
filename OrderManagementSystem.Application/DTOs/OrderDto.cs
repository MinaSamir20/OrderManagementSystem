#nullable disable
namespace OrderManagementSystem.Application.DTOs
{
    public class OrderDto
    {
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public List<string> PaymentMethod { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
