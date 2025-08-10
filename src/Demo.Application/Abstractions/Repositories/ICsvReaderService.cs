namespace Demo.Application.Abstractions;

public interface ICsvReaderService
{
    Task<List<T>> ReadAsync<T>(string filePath, CancellationToken ct = default);
}
