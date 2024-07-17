using OrderManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Services.Orders
{
    public interface IOrderService
    {
        Task<string> Add(Order order);
        IQueryable<Order> Get(int? id);
        Task<IEnumerable<Order>> GetAll(Expression<Func<Order, bool>>? criteria = null, string[]? includes = null);
        Task<string> Edit(Order order);
        Task<decimal> ProcessOrderAsync(int orderId);
        Task<bool> ValidateOrderAsync(Order order);
    }
}
