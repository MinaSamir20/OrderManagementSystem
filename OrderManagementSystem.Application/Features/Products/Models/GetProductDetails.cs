using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Products.Responces;

namespace OrderManagementSystem.Application.Features.Products.Models
{
    public class GetProductDetails(int id) : IRequest<Response<GetProductResponce>>
    {
        public int Id { get; set; } = id;
    }
}
