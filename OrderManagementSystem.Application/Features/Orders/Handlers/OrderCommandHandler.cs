using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Orders.Models;
using OrderManagementSystem.Application.Services.Orders;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Features.Orders.Handlers
{
    public class OrderCommandHandler(IOrderService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<CreateOrder, Response<string>>,
        IRequestHandler<UpdateOrder, Response<string>>
    {
        private readonly IOrderService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<string>> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            return Success<string>(await _service.Add(order));
        }

        public async Task<Response<string>> Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            var order = _service.Get(request.Id);
            if (order == null) return NotFound<string>("Order Not Found");
            var response = _mapper.Map<Order>(request);
            var result = await _service.Edit(response);
            if (result == "Updated Successfully") return Success<string>(result);
            return BadRequest<string>();
        }
    }
}
