#nullable disable
namespace OrderManagementSystem.Domain.Entities
{
    public class Order : BaseEnitiy
    {
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public List<string> PaymentMethod { get; set; }
        public bool Status { get; set; }

        //--Relations--//
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
