using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Services.Customers
{
    public interface ICustomerService
    {
        Task<string> Add(Customer customer);
        Task<IEnumerable<Order>> GetOrders(int customerId);
    }
}
