#nullable disable
namespace OrderManagementSystem.Domain.Entities
{
    public class OrderItem : BaseEnitiy
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }

        //--Relations--//
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
