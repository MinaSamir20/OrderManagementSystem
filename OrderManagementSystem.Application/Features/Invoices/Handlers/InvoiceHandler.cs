using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Invoices.Models;
using OrderManagementSystem.Application.Features.Invoices.Responces;
using OrderManagementSystem.Application.Services.Orders;

namespace OrderManagementSystem.Application.Features.Invoices.Handlers
{
    public class InvoiceHandler(IOrderService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<GetInvoiceDetails, Response<GetInvoiceResponce>>,
        IRequestHandler<GetInvoiceList, Response<IEnumerable<GetInvoiceResponce>>>

    {
        private readonly IOrderService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<IEnumerable<GetInvoiceResponce>>> Handle(GetInvoiceList request, CancellationToken cancellationToken)
        {
            var invoices = await _service.GetAll();
            return Success(_mapper.Map<IEnumerable<GetInvoiceResponce>>(invoices));
        }

        public Task<Response<GetInvoiceResponce>> Handle(GetInvoiceDetails request, CancellationToken cancellationToken)
        {
            var invoice = _service.Get(request.Id);
            return Task.FromResult(invoice == null ? NotFound<GetInvoiceResponce>("Invoice Not Found") : Success(_mapper.Map<GetInvoiceResponce>(invoice)));
        }
    }
}
