namespace Demo.Application.DTOs;

public class TreatmentCsvRow
{
    public string TreatmentIdentifier { get; set; } = string.Empty;
    public string? CompleteDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ItemCode { get; set; } = string.Empty;
    public string? Tooth { get; set; }
    public string? Surface { get; set; }
    public string Quantity { get; set; } = "0";
    public string Fee { get; set; } = "0";
    public string PatientIdentifier { get; set; } = string.Empty; // will resolve to PatientId
    public string IsPaid { get; set; } = "false";
    public string IsVoided { get; set; } = "false";
}
