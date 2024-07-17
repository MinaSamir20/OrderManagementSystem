using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Products.Models;
using OrderManagementSystem.Application.Services.Products;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Features.Products.Handlers
{
    public class ProductCommandHandler(IProductService service, IMapper mapper)
        : ResponseHandler, IRequestHandler<CreateProduct, Response<string>>,
        IRequestHandler<UpdateProduct, Response<string>>
    {
        private readonly IProductService _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<Response<string>> Handle(CreateProduct request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            return Success<string>(await _service.Add(product));
        }

        public async Task<Response<string>> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = _service.Get(request.Id);
            if (product == null) return NotFound<string>("Product Not Found");
            var response = _mapper.Map<Product>(request);
            var result = await _service.Edit(response);
            if (result == "Updated Successfully") return Success<string>(result);
            return BadRequest<string>();
        }
    }
}
