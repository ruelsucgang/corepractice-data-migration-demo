using Demo.Application.DTOs;

namespace Demo.Application.Abstractions.Services;

public interface IDataMigrationService
{
    // Ingest patients
    Task<int> SavePatientsAsync(IEnumerable<PatientCsvRow> patients, CancellationToken ct = default);

    // Ingest treatments and auto-create invoices per patient per day, then line items
    Task<(int TreatmentsSaved, int InvoicesCreated, int LineItemsCreated)>
        SaveTreatmentsWithInvoicingAsync(IEnumerable<TreatmentCsvRow> treatments, CancellationToken ct = default);
}
