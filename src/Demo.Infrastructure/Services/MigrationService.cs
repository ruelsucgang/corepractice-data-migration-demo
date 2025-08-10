using Demo.Application.Abstractions;
using Demo.Application.DTOs;
using Demo.Application.Validation;
using Demo.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Demo.Infrastructure.Services;

public class MigrationService : IMigrationService
{
    private readonly IRepository<Patient> _patientRepo;
    private readonly IRepository<Treatment> _treatmentRepo;
    private readonly IRepository<Invoice> _invoiceRepo;
    private readonly IRepository<InvoiceLineItem> _lineRepo;
    private readonly ILogger<MigrationService> _logger;

    private readonly PatientCsvValidator _patientValidator = new();
    private readonly TreatmentCsvValidator _treatmentValidator = new();

    public MigrationService(
        IRepository<Patient> patientRepo,
        IRepository<Treatment> treatmentRepo,
        IRepository<Invoice> invoiceRepo,
        IRepository<InvoiceLineItem> lineRepo,
        ILogger<MigrationService> logger)
    {
        _patientRepo = patientRepo;
        _treatmentRepo = treatmentRepo;
        _invoiceRepo = invoiceRepo;
        _lineRepo = lineRepo;
        _logger = logger;
    }

    public Task<(List<PatientCsvRow> valid, List<(PatientCsvRow row, List<ValidationIssue> issues)> invalid)>
        ValidatePatientsAsync(IEnumerable<PatientCsvRow> rows, CancellationToken ct = default)
    {
        var valid = new List<PatientCsvRow>();
        var invalid = new List<(PatientCsvRow row, List<ValidationIssue> issues)>();

        int i = 0;
        foreach (var r in rows)
        {
            var res = _patientValidator.Validate(r);
            if (res.IsValid) valid.Add(r);
            else
                invalid.Add((r, res.Errors.Select(e => new ValidationIssue(i, e.PropertyName, e.ErrorMessage)).ToList()));
            i++;
        }
        return Task.FromResult((valid, invalid));
    }

    public Task<(List<TreatmentCsvRow> valid, List<(TreatmentCsvRow row, List<ValidationIssue> issues)> invalid)>
        ValidateTreatmentsAsync(IEnumerable<TreatmentCsvRow> rows,
                                IEnumerable<Patient> existingPatients,
                                CancellationToken ct = default)
    {
        var patientIds = existingPatients.Select(p => p.PatientIdentifier).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var valid = new List<TreatmentCsvRow>();
        var invalid = new List<(TreatmentCsvRow row, List<ValidationIssue> issues)>();

        int i = 0;
        foreach (var r in rows)
        {
            var issues = new List<ValidationIssue>();
            var res = _treatmentValidator.Validate(r);
            if (!res.IsValid)
            {
                issues.AddRange(res.Errors.Select(e => new ValidationIssue(i, e.PropertyName, e.ErrorMessage)));
            }

            if (!patientIds.Contains(r.PatientIdentifier))
            {
                issues.Add(new ValidationIssue(i, nameof(r.PatientIdentifier), "Patient not found"));
            }

            if (issues.Count == 0) valid.Add(r);
            else invalid.Add((r, issues));
            i++;
        }

        return Task.FromResult((valid, invalid));
    }

    public async Task<MigrationResult> IngestAsync(
        IEnumerable<PatientCsvRow> patientRows,
        IEnumerable<TreatmentCsvRow> treatmentRows,
        CancellationToken ct = default)
    {
        // 1) Upsert patients by PatientIdentifier
        var existing = await _patientRepo.ListAsync(ct: ct);
        var existingMap = existing.ToDictionary(p => p.PatientIdentifier, StringComparer.OrdinalIgnoreCase);

        var toInsertPatients = new List<Patient>();
        foreach (var r in patientRows)
        {
            var key = (r.PatientIdentifier ?? "").Trim();
            if (string.IsNullOrEmpty(key)) continue;

            if (existingMap.TryGetValue(key, out var existingPatient))
            {
                // optional upsert/update (kept minimal)
                existingPatient.Firstname = r.Firstname.Trim();
                existingPatient.Lastname = r.Lastname.Trim();
                existingPatient.Email = NormalizeEmail(r.Email);
                continue;
            }

            var p = new Patient
            {
                PatientIdentifier = key,
                PatientNo = NullIfEmpty(r.PatientNo),
                Firstname = r.Firstname.Trim(),
                Lastname = r.Lastname.Trim(),
                Middlename = NullIfEmpty(r.Middlename),
                PreferredName = NullIfEmpty(r.PreferredName),
                DateOfBirth = TryDate(r.DateOfBirth),
                Title = NullIfEmpty(r.Title),
                Sex = NullIfEmpty(r.Sex),
                Email = NormalizeEmail(r.Email),
                HomePhone = NullIfEmpty(r.HomePhone),
                Mobile = NullIfEmpty(r.Mobile),
                Occupation = NullIfEmpty(r.Occupation),
                CompanyName = NullIfEmpty(r.CompanyName),
                AddressLine1 = NullIfEmpty(r.AddressLine1),
                AddressLine2 = NullIfEmpty(r.AddressLine2),
                Suburb = NullIfEmpty(r.Suburb),
                Postcode = NullIfEmpty(r.Postcode),
                State = NullIfEmpty(r.State),
                Country = NullIfEmpty(r.Country),
                IsDeleted = ParseBool(r.IsDeleted)
            };
            toInsertPatients.Add(p);
            existingMap[key] = p;
        }

        if (toInsertPatients.Count > 0)
            await _patientRepo.AddRangeAsync(toInsertPatients, ct);

        // IMPORTANT: save now so new Patients get real IDs
        await _patientRepo.SaveChangesAsync(ct);

        // 2) Map & insert treatments (now we can safely set PatientId)
        var toInsertTreatments = new List<Treatment>();
        foreach (var r in treatmentRows)
        {
            var key = (r.PatientIdentifier ?? "").Trim();
            if (!existingMap.TryGetValue(key, out var p))
            {
                _logger.LogWarning("Skipping treatment {Tx} - unknown patient {Pid}", r.TreatmentIdentifier, r.PatientIdentifier);
                continue;
            }

            var t = new Treatment
            {
                TreatmentIdentifier = r.TreatmentIdentifier,
                CompleteDate = TryDate(r.CompleteDate),
                Description = r.Description.Trim(),
                ItemCode = r.ItemCode.Trim(),
                Tooth = NullIfEmpty(r.Tooth),
                Surface = NullIfEmpty(r.Surface),
                Quantity = SafeInt(r.Quantity, 1),
                Fee = SafeDecimal(r.Fee, 0m),
                PatientId = p.PatientId,          // p has real ID now
                IsPaid = ParseBool(r.IsPaid),
                IsVoided = ParseBool(r.IsVoided)
            };
            toInsertTreatments.Add(t);
        }
        if (toInsertTreatments.Count > 0)
            await _treatmentRepo.AddRangeAsync(toInsertTreatments, ct);

        // Save so treatments get IDs (needed for line items)
        await _treatmentRepo.SaveChangesAsync(ct);

        // 3) Create invoices per (PatientId, CompleteDate.Date)
        var groups = toInsertTreatments
            .Where(t => t.CompleteDate.HasValue)
            .GroupBy(t => new { t.PatientId, Day = t.CompleteDate!.Value.Date });

        int nextInvoiceNo = 1;
        var toInsertInvoices = new List<Invoice>();
        var toInsertLines = new List<InvoiceLineItem>();

        foreach (var g in groups)
        {
            var invoice = new Invoice
            {
                InvoiceIdentifier = Guid.NewGuid().ToString("N"),
                InvoiceNo = nextInvoiceNo++,
                InvoiceDate = g.Key.Day,
                DueDate = g.Key.Day,
                Note = null,
                Total = 0m,
                Paid = 0m,
                Discount = 0m,
                PatientId = g.Key.PatientId,
                IsDeleted = false
            };

            decimal sum = 0m;
            foreach (var t in g)
            {
                var line = new InvoiceLineItem
                {
                    InvoiceLineItemIdentifier = Guid.NewGuid().ToString("N"),
                    Description = t.Description,
                    ItemCode = t.ItemCode,
                    Quantity = t.Quantity,
                    UnitAmount = t.Fee,
                    LineAmount = t.Fee * t.Quantity,
                    PatientId = t.PatientId,
                    TreatmentId = t.TreatmentId,   // now non-zero because we saved above
                    Invoice = invoice              // set nav so EF links properly
                };
                sum += line.LineAmount;
                toInsertLines.Add(line);

                t.Invoice = invoice; // link treatment to invoice (nav property)
            }

            invoice.Total = sum;
            toInsertInvoices.Add(invoice);
        }

        if (toInsertInvoices.Count > 0)
            await _invoiceRepo.AddRangeAsync(toInsertInvoices, ct);
        if (toInsertLines.Count > 0)
            await _lineRepo.AddRangeAsync(toInsertLines, ct);

        // final save
        await _patientRepo.SaveChangesAsync(ct);

        var patientsInserted = toInsertPatients.Count;
        var treatmentsInserted = toInsertTreatments.Count;
        var invoicesCreated = toInsertInvoices.Count;
        var linesCreated = toInsertLines.Count;

        return new MigrationResult(patientsInserted, treatmentsInserted, invoicesCreated, linesCreated);
    }

    #region helpers
    private static string? NullIfEmpty(string? s)
        => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

    private static DateTime? TryDate(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return null;
        // Allow common formats
        if (DateTime.TryParse(s, out var d)) return d;
        return null;
    }

    private static bool ParseBool(string? s)
    {
        if (string.IsNullOrWhiteSpace(s)) return false;
        return s.Trim().Equals("true", StringComparison.OrdinalIgnoreCase) ||
               s.Trim().Equals("1");
    }

    private static int SafeInt(string? s, int fallback)
        => int.TryParse(s, out var n) ? n : fallback;

    private static decimal SafeDecimal(string? s, decimal fallback)
        => decimal.TryParse(s, out var d) ? d : fallback;

    private static string? NormalizeEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email)) return null;
        var trimmed = email.Trim();
        return trimmed.Equals("n/a", StringComparison.OrdinalIgnoreCase) ? null : trimmed.ToLowerInvariant();
    }
    #endregion

}
