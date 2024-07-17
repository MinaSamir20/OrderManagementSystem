using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Customers.Models;
using OrderManagementSystem.Application.Services.Customers;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Features.Customers.Handlers
{
    public class CustomerCommandHandler(ICustomerService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<CreateCustomer, Response<string>>
    {
        private readonly ICustomerService _service = service;
        private readonly IMapper _mapper = mapper;
        public async Task<Response<string>> Handle(CreateCustomer request, CancellationToken cancellationToken)
        {
            var customer = _mapper.Map<Customer>(request);
            return Success<string>(await _service.Add(customer));
        }
    }
}
