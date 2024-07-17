using MediatR;

namespace OrderManagementSystem.Application.Features.Orders.Models
{
    public class ProcessOrderCommand : IRequest<decimal>
    {
        public int OrderId { get; set; }
        public ProcessOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
