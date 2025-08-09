namespace Demo.Domain.Entities;

public class Treatment
{
    public int TreatmentId { get; set; }                     // PK
    public string TreatmentIdentifier { get; set; } = null!;
    public DateTime? CompleteDate { get; set; }
    public string Description { get; set; } = null!;
    public string ItemCode { get; set; } = null!;
    public string? Tooth { get; set; }
    public string? Surface { get; set; }
    public int Quantity { get; set; }
    public decimal Fee { get; set; }

    public int? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public bool IsPaid { get; set; }
    public bool IsVoided { get; set; }
}
