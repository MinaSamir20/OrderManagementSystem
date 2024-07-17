using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Orders.Responces;

namespace OrderManagementSystem.Application.Features.Orders.Models
{
    public class GetOrderDetails(int id) : IRequest<Response<GetOrderResponce>>
    {
        public int Id { get; set; } = id;
    }
}
