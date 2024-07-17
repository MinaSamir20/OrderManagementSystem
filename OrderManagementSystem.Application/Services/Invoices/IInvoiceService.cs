using OrderManagementSystem.Domain.Entities;
using System.Linq.Expressions;

namespace OrderManagementSystem.Application.Services.Invoices
{
    public interface IInvoiceService
    {
        IQueryable<Invoice> Get(int? id);
        Task<IEnumerable<Invoice>> GetAll(Expression<Func<Invoice, bool>>? criteria = null, string[]? includes = null);
    }
}
