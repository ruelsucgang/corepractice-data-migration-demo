using Demo.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Persistence;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly CorePracticeDbContext _db;
    private readonly DbSet<T> _set;

    public Repository(CorePracticeDbContext db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    public Task<T> AddAsync(T entity, CancellationToken ct = default)
    {
        _set.Add(entity);
        return Task.FromResult(entity);
    }

    public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct = default)
    {
        _set.AddRange(entities);
        return Task.CompletedTask;
    }

    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        => _set.FirstOrDefaultAsync(predicate, ct)!;

    public async Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken ct = default)
        => predicate is null
            ? await _set.ToListAsync(ct)
            : await _set.Where(predicate).ToListAsync(ct);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);
}
