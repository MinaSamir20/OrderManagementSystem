using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Orders.Responces;

namespace OrderManagementSystem.Application.Features.Orders.Models
{
    public class GetOrderList : IRequest<Response<IEnumerable<GetOrderResponce>>>
    {
    }
}
