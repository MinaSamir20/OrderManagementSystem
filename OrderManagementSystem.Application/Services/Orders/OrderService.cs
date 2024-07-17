using OrderManagementSystem.Application.Services.Auth;
using OrderManagementSystem.Application.Services.Discounts;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Repositories.UnitOfWork;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Services.Orders
{
    public class OrderService(IUnitOfWork unit, DiscountService discountService, IEmailService emailService) : IOrderService
    {
        private readonly DiscountService _discountService = discountService;
        private readonly IEmailService _emailService = emailService;
        private readonly IUnitOfWork _unit = unit;
        public async Task<string> Add(Order order)
        {
            await _unit.Orders.CreateAsync(order);
            return "Created Successfully";
        }

        public async Task<string> Edit(Order order)
        {
            await _unit.Orders.Update(order);
            _unit.Complete();
            return "Updated Successfully";
        }

        public IQueryable<Order> Get(int? id) => _unit.Orders.GetTableNoTracking().Where(x => x.Id == id).AsQueryable();

        public async Task<IEnumerable<Order>> GetAll(Expression<Func<Order, bool>>? criteria = null, string[]? includes = null) => await _unit.Orders.GetAllAsync(criteria, includes);

        public async Task<bool> UpdateOrderStatusAsync(int orderId, bool newStatus)
        {
            var order = await _unit.Orders.GetAsync(orderId);
            if (order == null) return false;

            order.Status = newStatus;
            _unit.Complete();

            var customer = await _unit.Customers.GetAsync(order.CustomerId);
            if (customer != null)
            {
                string subject = "Order Status Update";
                string body = $"Dear {customer.Name},<br>Your order with ID {order.Id} status has been updated to {newStatus}.";
                await _emailService.SendEmailAsync(customer.Email, subject, body);
            }

            return true;
        }

        public async Task<bool> ValidateOrderAsync(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _unit.Products.GetAsync(orderItem.ProductId);
                if (product == null || product.Stock < orderItem.Quantity)
                {
                    return false; // Insufficient stock
                }
            }

            return true;
        }

        public async Task<decimal> ProcessOrderAsync(int orderId)
        {
            var order = await _unit.Orders.GetAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            var discountRate = _discountService.CalculateDiscount(order.TotalAmount);
            var discountedTotal = _discountService.ApplyDiscount(order.TotalAmount, discountRate);
            _unit.Complete();

            foreach (var orderItem in order.OrderItems)
            {
                var product = await _unit.Products.GetAsync(orderItem.ProductId);
                product.Stock -= orderItem.Quantity;
            }
            _unit.Complete();

            return discountedTotal;
        }
    }
}
