using MediatR;
using OrderManagementSystem.Application.Base;
using OrderManagementSystem.Application.Features.Orders.Responces;

namespace OrderManagementSystem.Application.Features.Customers.Models
{
    public class GetOrdersListWithCustomerId(int customerId) : IRequest<Response<IEnumerable<GetOrderResponce>>>
    {
        public int Id { get; set; } = customerId;
    }
}
