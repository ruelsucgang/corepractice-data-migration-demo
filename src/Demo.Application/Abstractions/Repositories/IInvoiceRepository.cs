using Demo.Domain.Entities;

namespace Demo.Application.Abstractions.Repositories;

public interface IInvoiceRepository
{
    Task AddRangeAsync(IEnumerable<Invoice> invoices, CancellationToken ct = default);
}
  