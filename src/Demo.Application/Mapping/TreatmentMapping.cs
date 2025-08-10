using Demo.Application.DTOs;
using Demo.Domain.Entities;

namespace Demo.Application.Mapping;

public static class TreatmentMapping
{
    public static Treatment ToEntity(this TreatmentCsvRow r, int patientId, Invoice invoice) =>
        new Treatment
        {
            TreatmentIdentifier = r.TreatmentIdentifier,
            CompleteDate = TryDate(r.CompleteDate),
            Description = r.Description,
            ItemCode = r.ItemCode,
            Tooth = r.Tooth,
            Surface = r.Surface,
            Quantity = int.TryParse(r.Quantity, out var q) ? q : 0,
            Fee = decimal.TryParse(r.Fee, out var f) ? f : 0,
            IsPaid = bool.TryParse(r.IsPaid, out var paid) && paid,
            IsVoided = bool.TryParse(r.IsVoided, out var voided) && voided,
            PatientId = patientId,
            Invoice = invoice
        };

    public static InvoiceLineItem ToLineItem(this Treatment t, int patientId, Invoice invoice) =>
        new InvoiceLineItem
        {
            Invoice = invoice,
            PatientId = patientId,
            Treatment = t,
            Description = t.Description,
            ItemCode = t.ItemCode,
            Quantity = t.Quantity,
            UnitAmount = t.Fee,
            LineAmount = t.Fee * t.Quantity,
            InvoiceLineItemIdentifier = $"LINE-{invoice.InvoiceNo}-{Guid.NewGuid():N}".Substring(0, 24)
        };

    private static DateTime? TryDate(string? s) => DateTime.TryParse(s, out var d) ? d : null;
}