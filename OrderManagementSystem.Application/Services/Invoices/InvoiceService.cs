using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Repositories.UnitOfWork;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Services.Invoices
{
    public class InvoiceService(IUnitOfWork unit) : IInvoiceService
    {
        private readonly IUnitOfWork _unit = unit;
        public IQueryable<Invoice> Get(int? id) => _unit.Invoices.GetTableNoTracking().Where(x => x.Id == id).AsQueryable();

        public async Task<IEnumerable<Invoice>> GetAll(Expression<Func<Invoice, bool>>? criteria = null, string[]? includes = null) => await _unit.Invoices.GetAllAsync(criteria, includes);
    }
}
