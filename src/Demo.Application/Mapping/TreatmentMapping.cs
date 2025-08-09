using Demo.Application.DTOs;
using Demo.Application.Validation;
using Demo.Domain.Entities;

namespace Demo.Application.Mapping;

public static class TreatmentMapping
{
    public static Treatment ToEntity(this TreatmentCsvRow row, int patientId)
    {
        DateTime? completed = null;
        if (!string.IsNullOrWhiteSpace(row.CompleteDate) && PatientValidator.TryParseDate(row.CompleteDate, out var d))
            completed = d;

        var quantity = int.TryParse(row.Quantity, out var q) ? q : 0;
        var fee = decimal.TryParse(row.Fee, out var f) ? f : 0m;

        return new Treatment
        {
            TreatmentIdentifier = row.TreatmentIdentifier.Trim(),
            CompleteDate = completed,
            Description = row.Description.Trim(),
            ItemCode = row.ItemCode.Trim(),
            Tooth = row.Tooth?.Trim(),
            Surface = row.Surface?.Trim(),
            Quantity = quantity,
            Fee = fee,
            PatientId = patientId,
            IsPaid = string.Equals(row.IsPaid, "true", StringComparison.OrdinalIgnoreCase),
            IsVoided = string.Equals(row.IsVoided, "true", StringComparison.OrdinalIgnoreCase)
        };
    }   
}
