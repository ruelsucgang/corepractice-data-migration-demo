using Demo.Application.DTOs;
using Demo.Domain.Entities;

namespace Demo.Application.Abstractions;

public record ValidationIssue(int RowIndex, string Field, string Message);
public record MigrationResult(
    int PatientsInserted,
    int TreatmentsInserted,
    int InvoicesCreated,
    int LineItemsCreated
);

public interface IMigrationService
{
    Task<(List<PatientCsvRow> valid,
          List<(PatientCsvRow row, List<ValidationIssue> issues)> invalid)>
        ValidatePatientsAsync(IEnumerable<PatientCsvRow> rows, CancellationToken ct = default);

    Task<(List<TreatmentCsvRow> valid,
          List<(TreatmentCsvRow row, List<ValidationIssue> issues)> invalid)>
        ValidateTreatmentsAsync(IEnumerable<TreatmentCsvRow> rows,
                                IEnumerable<Patient> existingPatients,
                                CancellationToken ct = default);

    Task<MigrationResult> IngestAsync(
        IEnumerable<PatientCsvRow> patientRows,
        IEnumerable<TreatmentCsvRow> treatmentRows,
        CancellationToken ct = default);
}


