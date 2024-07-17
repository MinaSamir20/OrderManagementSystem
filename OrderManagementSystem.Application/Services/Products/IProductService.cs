using OrderManagementSystem.Domain.Entities;
using System.Linq.Expressions;
namespace OrderManagementSystem.Application.Services.Products
{
    public interface IProductService
    {
        Task<string> Add(Product product);
        IQueryable<Product> Get(int? id);
        Task<IEnumerable<Product>> GetAll(Expression<Func<Product, bool>>? criteria = null, string[]? includes = null);
        Task<string> Edit(Product product);
    }
}
