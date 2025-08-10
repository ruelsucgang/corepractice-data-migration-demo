namespace Demo.Application.DTOs;

public class TreatmentCsvRow
{
    public string TreatmentIdentifier { get; set; } = "";
    public string? CompleteDate { get; set; }
    public string Description { get; set; } = "";
    public string ItemCode { get; set; } = "";
    public string? Tooth { get; set; }
    public string? Surface { get; set; }
    public string Quantity { get; set; } = "0";
    public string Fee { get; set; } = "0";
    public string PatientIdentifier { get; set; } = ""; // will resolve to PatientId
    public string IsPaid { get; set; } = "false";
    public string IsVoided { get; set; } = "false";
}
