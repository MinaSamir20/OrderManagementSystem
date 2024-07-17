using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Invoices.Responces;

namespace OrderManagementSystem.Application.Features.Invoices.Models
{
    public class GetInvoiceList : IRequest<Response<IEnumerable<GetInvoiceResponce>>>
    {
    }
}
