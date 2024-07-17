using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Repositories.UnitOfWork;

namespace OrderManagementSystem.Application.Services.Customers
{
    public class CustomerService(IUnitOfWork unit) : ICustomerService
    {
        private readonly IUnitOfWork _unit = unit;
        public async Task<string> Add(Customer customer)
        {
            await _unit.Customers.CreateAsync(customer);
            return "Created Successfully";
        }

        public async Task<IEnumerable<Order>> GetOrders(int customerId) => await _unit.Orders.GetAllAsync(o => o.CustomerId == customerId, null);
    }
}
