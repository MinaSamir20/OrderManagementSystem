using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Customers.Models;
using OrderManagementSystem.Application.Features.Orders.Responces;
using OrderManagementSystem.Application.Services.Customers;

namespace OrderManagementSystem.Application.Features.Customers.Handlers
{
    public class CustomerHandler(ICustomerService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<GetOrdersListWithCustomerId, Response<IEnumerable<GetOrderResponce>>>
    {
        private readonly ICustomerService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<IEnumerable<GetOrderResponce>>> Handle(GetOrdersListWithCustomerId request, CancellationToken cancellationToken)
        {
            var orders = await _service.GetOrders(request.Id);
            if (orders == null) return null!;
            return Success(_mapper.Map<IEnumerable<GetOrderResponce>>(orders));
        }
    }
}
