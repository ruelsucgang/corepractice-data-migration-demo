using Demo.Application.DTOs;
using Demo.Application.Validation;

namespace Demo.Application.Abstractions.Services;

public interface ICsvTreatmentService
{
    Task<(IReadOnlyList<TreatmentCsvRow> Rows, IReadOnlyList<ValidationError> Errors)>
        LoadAndValidateAsync(string csvPath, CancellationToken ct = default);
}
 