#nullable disable
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Features.Customers.Responces
{
    public class GetOrdersResponce
    {
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public bool Status { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
