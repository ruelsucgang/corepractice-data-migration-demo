using Demo.Application.DTOs;

namespace Demo.Application.Abstractions;

public interface ICsvReaderService
{

    Task<List<PatientCsvRow>> ReadPatientsAsync(string filePath, CancellationToken ct = default);
    Task<List<TreatmentCsvRow>> ReadTreatmentsAsync(string filePath, CancellationToken ct = default);

}



