using CsvHelper;
using CsvHelper.Configuration;
using Demo.Application.Abstractions;
using System.Globalization;

namespace Demo.Infrastructure.Services;

public class CsvReaderService : ICsvReaderService
{
    public async Task<List<T>> ReadAsync<T>(string filePath, CancellationToken ct = default)
    {
        using var reader = new StreamReader(filePath);
        var cfg = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            TrimOptions = TrimOptions.Trim | TrimOptions.InsideQuotes,
            DetectDelimiter = true,
            BadDataFound = null
        };
        using var csv = new CsvReader(reader, cfg);
        var records = new List<T>();
        await foreach (var r in csv.GetRecordsAsync<T>(ct))
            records.Add(r);
        return records;
    }
}
