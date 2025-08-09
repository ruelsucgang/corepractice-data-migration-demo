using Demo.Domain.Entities;

namespace Demo.Application.Abstractions.Repositories;

public interface IInvoiceLineItemRepository
{
    Task AddRangeAsync(IEnumerable<InvoiceLineItem> lineItems, CancellationToken ct = default);
}
