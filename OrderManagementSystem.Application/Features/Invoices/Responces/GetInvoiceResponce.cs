#nullable disable
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Features.Invoices.Responces
{
    public class GetInvoiceResponce
    {
        public int Id { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
