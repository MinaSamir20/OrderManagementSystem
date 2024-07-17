#nullable disable
namespace OrderManagementSystem.Domain.Entities
{
    public class Invoice : BaseEnitiy
    {
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        //--Relations--//
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
