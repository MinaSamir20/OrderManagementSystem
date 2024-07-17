using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Invoices.Responces;

namespace OrderManagementSystem.Application.Features.Invoices.Models
{
    public class GetInvoiceDetails(int id) : IRequest<Response<GetInvoiceResponce>>
    {
        public int Id { get; set; } = id;
    }
}
