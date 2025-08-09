using Demo.Domain.Entities;

namespace Demo.Application.Abstractions.Repositories;

public interface IPatientRepository
{
    Task<Patient?> FindByIdentifierAsync(string patientIdentifier, CancellationToken ct = default);
    Task AddRangeAsync(IEnumerable<Patient> patients, CancellationToken ct = default);
}
