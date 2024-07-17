using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Repositories.UnitOfWork;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Services.Products
{
    public class ProductService(IUnitOfWork unit) : IProductService
    {
        private readonly IUnitOfWork _unit = unit;
        public async Task<string> Add(Product product)
        {
            await _unit.Products.CreateAsync(product);
            return "Created Successfully";
        }

        public async Task<string> Edit(Product product)
        {
            await _unit.Products.Update(product);
            _unit.Complete();
            return "Updated Successfully";
        }

        public IQueryable<Product> Get(int? id) => _unit.Products.GetTableNoTracking().Where(x => x.Id == id).AsQueryable();

        public async Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>>? criteria = null, string[]? includes = null) => await _unit.Products.GetAllAsync(criteria, includes);
    }
}
