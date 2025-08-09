using Demo.Domain.Entities;

namespace Demo.Application.Abstractions.Repositories;

public interface ITreatmentRepository
{
    Task AddRangeAsync(IEnumerable<Treatment> treatments, CancellationToken ct = default);
}
