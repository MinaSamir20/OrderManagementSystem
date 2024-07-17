using AutoMapper;
using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Products.Models;
using OrderManagementSystem.Application.Features.Products.Responces;
using OrderManagementSystem.Application.Services.Products;

namespace OrderManagementSystem.Application.Features.Products.Handlers
{
    public class ProductHandler(IProductService service, IMapper mapper) : ResponseHandler, IRequestHandler<GetProductDetails, Response<GetProductResponce>>,
        IRequestHandler<GetProductList, Response<IEnumerable<GetProductResponce>>>
    {
        private readonly IProductService _service = service;
        private readonly IMapper _mapper = mapper;

        public Task<Response<GetProductResponce>> Handle(GetProductDetails request, CancellationToken cancellationToken)
        {
            var product = _service.Get(request.Id);
            return Task.FromResult(product == null ? NotFound<GetProductResponce>("Invoice Not Found") : Success(_mapper.Map<GetProductResponce>(product)));
        }

        public async Task<Response<IEnumerable<GetProductResponce>>> Handle(GetProductList request, CancellationToken cancellationToken)
        {
            var products = await _service.GetAll();
            return Success(_mapper.Map<IEnumerable<GetProductResponce>>(products));
        }
    }
}
