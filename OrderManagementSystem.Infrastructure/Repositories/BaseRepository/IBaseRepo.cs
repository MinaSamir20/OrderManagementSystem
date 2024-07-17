using System.Linq.Expressions;

namespace OrderManagementSystem.Infrastructure.Repositories.BaseRepository
{
    public interface IBaseRepo<T> where T : class
    {
        Task<string> CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? criteria, string[]? includes);
        Task<string> Update(T entity);
        IQueryable<T> GetTableNoTracking();
    }
}
