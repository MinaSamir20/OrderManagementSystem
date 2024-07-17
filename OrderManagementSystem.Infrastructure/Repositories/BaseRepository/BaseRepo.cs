using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Databases;
using System.Linq.Expressions;

namespace OrderManagementSystem.Infrastructure.Repositories.BaseRepository
{
    public class BaseRepo<T>(AppDbContext db) : IBaseRepo<T> where T : BaseEnitiy
    {
        private readonly AppDbContext _db = db;
        public async Task<string> CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return "Created Successfully";
        }

        public async Task<T> GetAsync(int id) => (await GetTableNoTracking().Where(a => a.Id == id).SingleOrDefaultAsync())!;

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? criteria, string[]? includes)
        {
            var query = GetTableNoTracking();
            if (includes is not null)
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            if (criteria is not null) query = query.Where(criteria);
            return (await query.ToListAsync())!;
        }

        public async Task<string> Update(T entity)
        {
            var t = await _db.Set<T>().FindAsync(entity.Id);
            if (t is null) _db.Update(entity);
            return "Updated";
        }

        public IQueryable<T> GetTableNoTracking() => _db.Set<T>().AsNoTracking().AsQueryable();
    }
}
