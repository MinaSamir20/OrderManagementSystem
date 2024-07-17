using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Orders.Models;
using OrderManagementSystem.Application.Features.Orders.Responces;
using OrderManagementSystem.Application.Services.Orders;

namespace OrderManagementSystem.Application.Features.Orders.Handlers
{
    public class OrderHandler(IOrderService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<GetOrderDetails, Response<GetOrderResponce>>,
        IRequestHandler<GetOrderList, Response<IEnumerable<GetOrderResponce>>>
    {
        private readonly IOrderService _service = service;
        private readonly IMapper _mapper = mapper;
        public Task<Response<GetOrderResponce>> Handle(GetOrderDetails request, CancellationToken cancellationToken)
        {
            var order = _service.Get(request.Id);
            return Task.FromResult(order == null ? NotFound<GetOrderResponce>("Invoice Not Found") : Success(_mapper.Map<GetOrderResponce>(order)));
        }

        public async Task<Response<IEnumerable<GetOrderResponce>>> Handle(GetOrderList request, CancellationToken cancellationToken)
        {
            var orders = await _service.GetAll();
            return Success(_mapper.Map<IEnumerable<GetOrderResponce>>(orders));
        }
    }
}
