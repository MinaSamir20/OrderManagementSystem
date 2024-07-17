using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Databases;
using OrderManagementSystem.Infrastructure.Repositories.BaseRepository;

namespace OrderManagementSystem.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public IBaseRepo<Customer> Customers { get; private set; }

        public IBaseRepo<Invoice> Invoices { get; private set; }

        public IBaseRepo<Order> Orders { get; private set; }

        public IBaseRepo<Product> Products { get; private set; }
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Customers = new BaseRepo<Customer>(_db);
            Invoices = new BaseRepo<Invoice>(_db);
            Orders = new BaseRepo<Order>(_db);
            Products = new BaseRepo<Product>(_db);
        }

        public int Complete() => _db.SaveChanges();

        public void Dispose() => _db.Dispose();
    }
}
