using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Repositories.BaseRepository;

namespace OrderManagementSystem.Infrastructure.Repositories.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepo<Customer> Customers { get; }
        IBaseRepo<Invoice> Invoices { get; }
        IBaseRepo<Order> Orders { get; }
        IBaseRepo<Product> Products { get; }
        int Complete();
    }
}
