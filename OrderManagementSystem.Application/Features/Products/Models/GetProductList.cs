using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Products.Responces;

namespace OrderManagementSystem.Application.Features.Products.Models
{
    public class GetProductList : IRequest<Response<IEnumerable<GetProductResponce>>>
    {
    }
}
